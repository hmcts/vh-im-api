using System;
using InstantMessagingAPI.Domain.Ddd;

namespace InstantMessagingAPI.Domain
{
    public class InstantMessage : Entity<Guid>
    {
        public string From { get; set; }

        public string To { get; set; }

        // public Group Group { get; set; }

        public string MessageText { get; set; }

        // public bool IsAnnouncement { get; set; }

        public Guid ConferenceId { set; get; }

        //public DateTime TimeStamp { get; }

        //public InstantMessage(string sender, string receiver, Group group, string messageText, bool isAnnouncement)
        //{
        //    Id = Guid.NewGuid();
        //    Sender = sender;
        //    Receiver = receiver;
        //    Group = group;
        //    MessageText = messageText;
        //    IsAnnouncement = isAnnouncement;
        //}

        public InstantMessage(string from, string to, string messageText)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            MessageText = messageText;
        }
    }
}
