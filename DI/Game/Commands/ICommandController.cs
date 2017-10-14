using AuroraEmu.Game.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.DI.Game.Commands
{
    public interface ICommandController
    {
        bool TryHandleCommand(Client client, string input);
    }
}
