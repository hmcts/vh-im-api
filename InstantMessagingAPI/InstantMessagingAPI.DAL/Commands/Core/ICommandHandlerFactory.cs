﻿using System.Threading.Tasks;

namespace InstantMessagingAPI.DAL.Commands.Core
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Create<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
