using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Players;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Messenger
{
    public interface IMessengerController
    {
        void MessengerSearch(string searchString, Player player, List<MessengerSearch> friends, List<MessengerSearch> notFriends);

        List<MessengerSearch> SearchForUsers(string searchString);
    }
}