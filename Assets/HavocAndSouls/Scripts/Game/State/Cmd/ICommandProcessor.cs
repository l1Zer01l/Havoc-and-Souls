/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace HavocAndSouls
{
    public interface ICommandProcessor
    {
        void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;

        bool Process<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
