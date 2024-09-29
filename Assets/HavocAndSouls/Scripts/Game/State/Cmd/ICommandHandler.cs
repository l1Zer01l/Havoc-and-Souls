/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace HavocAndSouls
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        bool Handle(TCommand command);
    }
}
