using System;
using System.Collections.Generic;
using System.Text;

namespace InstantMessagingAPI.Domain.Ddd
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }
    }
}
