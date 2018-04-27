using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IPlayerDao
    {
        Player GetPlayerById(int id);

        Player GetPlayerBySSO(string sso);

        string GetPlayerNameById(int id, out string name);

        Player GetPlayerByName(string username);

        void UpdateCurrency(int playerId, int amount, string type);
        void UpdateHomeRoom(int playerId, int homeRoom);
    }
}