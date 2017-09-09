using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Players;

namespace AuroraEmu.Database.DAO
{
    public class PlayerDao : IPlayerDao
    {
        public Player GetPlayerById(int id)
        {
            Player player = null;
            using (DatabaseConnection dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery(
                    "SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE id = @id;");
                dbClient.AddParameter("@id", id);
                using (var reader = dbClient.ExecuteReader())
                    if (reader.Read())
                        player = new Player(reader);
            }
            
            return player;
        }

        public Player GetPlayerBySSO(string sso)
        {
            Player player = null;
            using (DatabaseConnection dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE sso_ticket = @sso_ticket;");
                dbClient.AddParameter("@sso_ticket", sso);
                using (var reader = dbClient.ExecuteReader())
                    if (reader.Read())
                        player = new Player(reader);
            }

            return player;
        }

        public string GetPlayerNameById(int id, out string name)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery("SELECT username FROM players WHERE id = @id LIMIT 1");
                dbClient.AddParameter("@id", id);
                name = dbClient.GetString();
            }
            
            return name;
        }

        public Player GetPlayerByName(string username)
        {
            Player player = null;
            using (DatabaseConnection dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE username = @username;");
                dbClient.AddParameter("@username", username);
                using (var reader = dbClient.ExecuteReader())
                    if (reader.Read())
                        player = new Player(reader);
            }
            return player;
        }

        public void UpdateCurrency(int playerId, int amount, string type)
        {
            using (DatabaseConnection dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery($"UPDATE players SET {type} = @amount WHERE id = @playerId");
                dbClient.AddParameter("@amount", amount);
                dbClient.AddParameter("@playerId", playerId);
                dbClient.Execute();
            }
        }
    }
}