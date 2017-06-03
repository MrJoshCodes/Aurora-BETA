using AuroraEmu.Database;
using System.Collections.Concurrent;
using System.Data;

namespace AuroraEmu.Game.Players
{
    public class PlayerController
    {
        private static PlayerController instance;

        private ConcurrentDictionary<int, Player> playersById;
        private ConcurrentDictionary<int, string> playerNamesById;

        public PlayerController()
        {
            playersById = new ConcurrentDictionary<int, Player>();
            playerNamesById = new ConcurrentDictionary<int, string>();
        }

        public Player GetPlayerById(int id)
        {
            if (playersById.TryGetValue(id, out Player player))
                return player;

            DataRow result = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, sso_ticket FROM players WHERE id = @id;");
                dbClient.AddParameter("@id", id);
                dbClient.Open();

                result = dbClient.GetRow();
            }

            if (result != null)
            {
                player = new Player(result);
                playersById.TryAdd(player.Id, player);
                playerNamesById.TryAdd(player.Id, player.Username);

                return player;
            }

            return null;
        }

        public Player GetPlayerBySSO(string sso)
        {
            DataRow result = null;

            using (var dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, sso_ticket FROM players WHERE sso_ticket = @sso_ticket;");
                dbClient.AddParameter("@sso_ticket", sso);
                dbClient.Open();

                result = dbClient.GetRow();
            }

            if (result != null)
            {
                Player player = new Player(result);
                playersById.AddOrUpdate(player.Id, player, (oldkey, oldvalue) => player);
                playerNamesById.AddOrUpdate(player.Id, player.Username, (oldkey, oldvalue) => player.Username);
                return player;
            }

            return null;
        }

        public string GetPlayerNameById(int id)
        {
            if (playerNamesById.TryGetValue(id, out string name))
                return name;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT username FROM players WHERE id = @id LIMIT 1");
                dbClient.AddParameter("@id", id);
                name = dbClient.GetString();
            }

            return name;
        }

        public static PlayerController GetInstance()
        {
            if (instance == null)
                instance = new PlayerController();

            return instance;
        }
    }
}
