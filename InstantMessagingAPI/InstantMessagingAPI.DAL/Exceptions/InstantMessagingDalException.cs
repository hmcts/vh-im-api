using System;
using System.Runtime.Serialization;
namespace InstantMessagingAPI.DAL.Exceptions
{
    public abstract class InstantMessagingDalException : Exception
    {
        protected InstantMessagingDalException(string message) : base(message)
        {
        }

        protected InstantMessagingDalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
