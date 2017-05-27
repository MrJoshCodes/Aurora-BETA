using AuroraEmu.Database;
using AuroraEmu.Game.Players;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Storage.Players
{
    public class PlayerDao
    {
        public static Player GetPlayerById(int id)
        {
            DataRow result = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.WriteQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, sso_ticket FROM players WHERE id = @id;");
                dbClient.AddParameter("", id);
                dbClient.Open();
            }
            return new Player(result);
        }

        public static Player GetPlayerBySSO(string sso)
        {
            DataRow result = null;

            using (var dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.WriteQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, sso_ticket FROM players WHERE sso_ticket = @sso_ticket;");
                dbClient.AddParameter("@sso_ticket", sso);
                dbClient.Open();

                result = dbClient.GetRow();
            }
            return new Player(result);
        }
    }
}
