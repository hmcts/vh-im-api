using System;
using InstantMessagingAPI.Domain.Ddd;

namespace InstantMessagingAPI.Domain
{
    public class User : Entity<Guid>
    {
        public string Name { get; set; }

        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
