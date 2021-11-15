using System;
using InstantMessagingAPI.DAL.Commands.Core;
using InstantMessagingAPI.Domain;

namespace InstantMessagingAPI.DAL.Commands
{
    public class AddInstantMessageCommand : ICommand
    {
        public AddInstantMessageCommand(Guid conferenceId, string from, string messageText, string to)
        {
            ConferenceId = conferenceId;
            From = from;
            MessageText = messageText;
            To = to;
        }

        public string From { get; set; }
        public string MessageText { get; set; }
        public Guid ConferenceId { get; }
        public string To { get; set; }
    }

    public class AddInstantMessageCommandHandler : ICommandHandler<AddInstantMessageCommand>
    {
        private readonly InstantMessagingAPIDbContext _context;

        public AddInstantMessageCommandHandler(InstantMessagingAPIDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task Handle(AddInstantMessageCommand command)
        {
            var @instantMessage = new InstantMessage(command.From, command.MessageText, command.To)
            {
                ConferenceId = command.ConferenceId
            };
            await _context.AddAsync(@instantMessage);
            await _context.SaveChangesAsync();
        }
    }
}
