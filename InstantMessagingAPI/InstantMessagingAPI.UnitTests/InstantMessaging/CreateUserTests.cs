using FluentAssertions;
using NUnit.Framework;
using InstantMessagingAPI.Domain;

namespace InstantMessagingAPI.UnitTests.InstantMessaging
{
    public class CreateUserTests
    {
        [Test]
        public void should_create_user_with_name()
        {
            var name = "User 1";

            var user = new User(name);

            user.Id.Should().NotBeEmpty();
            user.Name.Should().Be(name);
        }
    }
}
