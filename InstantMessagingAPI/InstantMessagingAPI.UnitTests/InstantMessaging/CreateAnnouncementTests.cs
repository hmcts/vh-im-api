using FluentAssertions;
using NUnit.Framework;
using InstantMessagingAPI.Domain;

namespace InstantMessagingAPI.UnitTests.InstantMessaging
{
    public class CreateAnnouncementTests
    {
        [Test]
        public void should_create_annoucement_with_messagetext()
        {
            var messageText = "Sample Broadcast Message";

            var announcement = new Announcement(messageText);

            announcement.Id.Should().NotBeEmpty();
            announcement.MessageText.Should().Be(messageText);
        }
    }
}
