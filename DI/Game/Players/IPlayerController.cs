using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.DI.Game.Players
{
    public interface IPlayerController
    {
        IPlayerDao Dao { get; }

        Player GetPlayerById(int id);

        Player GetPlayerBySSO(string sso);

        string GetPlayerNameById(int id);

        Player GetPlayerByName(string name);
    }
}
