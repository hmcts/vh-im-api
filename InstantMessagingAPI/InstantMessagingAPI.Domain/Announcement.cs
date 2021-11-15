using System;
using InstantMessagingAPI.Domain.Ddd;

namespace InstantMessagingAPI.Domain
{
    public class Announcement : Entity<Guid>
    {
        public string MessageText { get; set; }

        public Announcement(string messageText)
        {
            Id = Guid.NewGuid();
            MessageText = messageText;
        }
    }
}
