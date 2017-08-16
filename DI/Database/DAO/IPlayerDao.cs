using System.Data;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IPlayerDao
    {
        DataRow GetPlayerById(int id);

        DataRow GetPlayerBySSO(string sso);

        string GetPlayerNameById(int id, out string name);

        DataRow GetPlayerByName(string username);
    }
}