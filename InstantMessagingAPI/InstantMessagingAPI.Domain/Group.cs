using System;
using InstantMessagingAPI.Domain.Ddd;

namespace InstantMessagingAPI.Domain
{
    public class Group : Entity<Guid>
    {
        public string Name { get; set; }

        public Group(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
