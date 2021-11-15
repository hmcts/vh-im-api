using FluentAssertions;
using NUnit.Framework;
using InstantMessagingAPI.Domain;

namespace InstantMessagingAPI.UnitTests.InstantMessaging
{
    public class CreateGroupTests
    {
        [Test]
        public void should_create_group_with_name()
        {
            var name = "Group 1";

            var group = new Group(name);

            group.Id.Should().NotBeEmpty();
            group.Name.Should().Be(name);
        }
    }
}
