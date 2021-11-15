using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using InstantMessagingAPI.Common.Configuration;
using InstantMessagingAPI.Extensions;
using InstantMessagingAPI.Middleware;
using InstantMessagingAPI.Middleware.Validation;
using InstantMessagingAPI.DAL;
using InstantMessagingAPI.Middleware.Logging;

namespace InstantMessagingAPI
{
    public class Startup
    {
      
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public SettingsConfiguration SettingsConfiguration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddSwaggerDocument();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
               builder =>
               {
                   builder
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed(host => true)
                       .AllowCredentials();
               }));

            services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);
            services.AddJsonOptions();
            RegisterSettings(services);
            services.AddCustomTypes();
            // TODO: Bridge once all apps and azure configs are made
            // RegisterAuth(services);

            services.AddTransient<IRequestModelValidatorService, RequestModelValidatorService>();

            // TODO: Correct the Exception thrown
            // services.AddMvc(opt => opt.Filters.Add(typeof(LoggingMiddleware))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(opt => opt.Filters.Add(typeof(RequestModelValidatorFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IRequestModelValidatorService>());
            services.AddTransient<IValidatorFactory, RequestModelValidatorFactory>();

            services.AddDbContextPool<InstantMessagingAPIDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VhInstantMessagingApi")));
        }

        private void RegisterAuth(IServiceCollection serviceCollection)
        {
            var securitySettings = Configuration.GetSection("AzureAd").Get<AzureAdConfiguration>();
            var serviceSettings = Configuration.GetSection("Services").Get<ServicesConfiguration>();
            var imSettings = Configuration.GetSection("InstantMessagingConfiguration").Get<InstantMessagingConfiguration>();

            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"{securitySettings.Authority}{securitySettings.TenantId}";
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    ValidAudience = serviceSettings.VhInstantmessagingApiResourceId
                };
            });

            serviceCollection.AddAuthorization(AddPolicies);

            serviceCollection.AddMvc(AddMvcPolicies);
        }

        private void RegisterSettings(IServiceCollection services)
        {
            SettingsConfiguration = Configuration.Get<SettingsConfiguration>();
            services.Configure<AzureAdConfiguration>(options => Configuration.Bind("AzureAd", options));
            services.Configure<ServicesConfiguration>(options => Configuration.Bind("Services", options));
            services.Configure<InstantMessagingConfiguration>(options => Configuration.Bind("InstantMessagingConfiguration", options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // TODO : When handling migrations
            // app.RunLatestMigrations();
            app.UseOpenApi();
            app.UseSwaggerUi3(c =>
            {
                c.DocumentTitle = "Instant Messaging API V1";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (!SettingsConfiguration.DisableHttpsRedirection)
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();
            app.UseCors("CorsPolicy");

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }

        private static void AddPolicies(AuthorizationOptions options)
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        }

        private static void AddMvcPolicies(MvcOptions options)
        {
            options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser().Build()));
        }
    }
}
