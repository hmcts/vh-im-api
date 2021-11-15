using System.Collections.Generic;

namespace InstantMessagingAPI.Domain.Validations
{
    public class ValidationFailures : List<ValidationFailure>
    {
        public void AddFailure(string name, string message)
        {
            Add(new ValidationFailure(name, message));
        }
    }
}
