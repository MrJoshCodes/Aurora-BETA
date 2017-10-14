using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Messenger.Models;
using AuroraEmu.Game.Players.Models;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Messenger
{
    public interface IMessengerController
    {
        IMessengerDao Dao { get; }

        void MessengerSearch(string searchString, Player player, List<MessengerSearch> friends, List<MessengerSearch> notFriends);

        List<MessengerSearch> SearchForUsers(string searchString);
    }
}