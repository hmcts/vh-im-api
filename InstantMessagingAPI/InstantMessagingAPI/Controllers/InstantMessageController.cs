using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using InstantMessagingAPI.Contract.Requests;
using InstantMessagingAPI.DAL.Commands;
using InstantMessagingAPI.DAL.Commands.Core;
using InstantMessagingAPI.DAL.Queries.Core;

namespace InstantMessagingAPI.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("instantmessage")]
    [ApiController]
    public class InstantMessageController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;
        private readonly ILogger<InstantMessageController> _logger;

        public InstantMessageController(IQueryHandler queryHandler, ICommandHandler commandHandler,
            ILogger<InstantMessageController> logger)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _logger = logger;
        }

        /// <summary>
        /// Saves chat message exchanged between participants
        /// </summary>
        /// <param name="conferenceId">Id of the conference</param>
        /// <param name="request">Details of the chat message</param>
        /// <returns>OK if the message is saved successfully</returns>
        [HttpPost("AddInstantMessageToConference")]
        [OpenApiOperation("AddInstantMessageToConference")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddInstantMessageToConferenceAsync(Guid conferenceId, AddInstantMessageRequest request)
        {
            _logger.LogDebug("Saving instant message");

            try
            {
                var command = new AddInstantMessageCommand(conferenceId, request.From, request.MessageText, request.To);
                await _commandHandler.Handle(command);

                return Ok("InstantMessage saved");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to add instant messages");
                return BadRequest();
            }
        }
    }
}
