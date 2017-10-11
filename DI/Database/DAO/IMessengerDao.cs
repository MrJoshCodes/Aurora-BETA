using System.Collections.Generic;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger.Models;
using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IMessengerDao
    {
        List<MessengerSearch> SearchForUsers(string searchString);

        Dictionary<int, MessengerFriend> GetFriendsById(int id, Dictionary<int, MessengerFriend> friends);

        Dictionary<int, MessengerRequest> GetRequestsByPlayerId(int playerId,
            Dictionary<int, MessengerRequest> requests);

        void CreateFriendship(Player player, int userTwo);

        void DestroyRequest(int userOne, int userTwo);

        void DestroyAllRequests(int userOne);

        void CreateRequest(int toId, Client client);

        void DestroyFriendship(int userOne, int userTwo);
    }
}