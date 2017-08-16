using System.Data;
using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.Database.DAO
{
    public class PlayerDao : IPlayerDao
    {
        public DataRow GetPlayerById(int id)
        {
            DataRow result;
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery(
                    "SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE id = @id;");
                dbClient.AddParameter("@id", id);
                result = dbClient.GetRow();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
            
            return result;
        }

        public DataRow GetPlayerBySSO(string sso)
        {
            DataRow result;

            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE sso_ticket = @sso_ticket;");
                dbClient.AddParameter("@sso_ticket", sso);
                result = dbClient.GetRow();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
            return result;
        }

        public string GetPlayerNameById(int id, out string name)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("SELECT username FROM players WHERE id = @id LIMIT 1");
                dbClient.AddParameter("@id", id);
                name = dbClient.GetString();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
            
            return name;
        }

        public DataRow GetPlayerByName(string username)
        {
            DataRow row;
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE username = @username;");
                dbClient.AddParameter("@username", username);
                row = dbClient.GetRow();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
            return row;
        }
    }
}