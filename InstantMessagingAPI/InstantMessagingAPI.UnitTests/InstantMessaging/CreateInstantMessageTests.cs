using FluentAssertions;
using NUnit.Framework;
using InstantMessagingAPI.Domain;

namespace InstantMessagingAPI.UnitTests.InstantMessaging
{
    public class CreateInstantMessageTests
    {
        [Test]
        public void should_create_instantmessage_with_receiver_sender_and_message()
        {
            var from = "User 1";
            var to = "User 2";
            var group = new Group("Group1");
            var message = "Test Message";

            //var instantMessage = new InstantMessage(sender, receiver, group, message, false);

            var instantMessage = new InstantMessage(from, to, message);

            instantMessage.Id.Should().NotBeEmpty();
            instantMessage.From.Should().Be(from);
            instantMessage.To.Should().Be(to);
            //instantMessage.Group.Should().Be(group);
            //instantMessage.IsAnnouncement.Should().Be(false);
        }
    }
}
