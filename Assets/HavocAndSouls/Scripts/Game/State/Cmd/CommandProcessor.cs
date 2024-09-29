/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using System.Collections.Generic;

namespace HavocAndSouls
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly Dictionary<Type, object> m_handlesMap = new ();
        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            m_handlesMap[typeof(TCommand)] = handler;
        }

        public bool Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (m_handlesMap.TryGetValue(typeof(TCommand), out var handler))
            {
                var typedHandler = handler as ICommandHandler<TCommand>;
                return typedHandler.Handle(command);          
            }
            return false;
        }
       
    }
}
