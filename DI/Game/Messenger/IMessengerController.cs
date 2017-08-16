using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Network.Game.Packets;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Messenger
{
    public interface IMessengerController
    {
        MessageComposer MessengerSearch(string searchString, Client client);

        List<MessengerSearch> SearchForUsers(string searchString);
    }
}