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
        private ConcurrentDictionary<string, Player> playersByName;

        public PlayerController()
        {
            playersById = new ConcurrentDictionary<int, Player>();
            playerNamesById = new ConcurrentDictionary<int, string>();
            playersByName = new ConcurrentDictionary<string, Player>();
        }

        public Player GetPlayerById(int id)
        {
            if (playersById.TryGetValue(id, out Player player))
                return player;

            DataRow result = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE id = @id;");
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
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE sso_ticket = @sso_ticket;");
                dbClient.AddParameter("@sso_ticket", sso);
                dbClient.Open();

                result = dbClient.GetRow();
            }

            if (result != null)
            {
                Player player = new Player(result);
                playersById.AddOrUpdate(player.Id, player, (oldkey, oldvalue) => player);
                playerNamesById.AddOrUpdate(player.Id, player.Username, (oldkey, oldvalue) => player.Username);
                playersByName.AddOrUpdate(player.Username, player, (oldkey, oldvalue) => player);
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
                dbClient.Open();

                name = dbClient.GetString();
            }

            return name;
        }

        public Player GetPlayerByName(string name)
        {
            if (playersByName.TryGetValue(name, out Player player))
                return player;

            DataRow result = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket FROM players WHERE username = @username;");
                dbClient.AddParameter("@username", name);
                dbClient.Open();

                result = dbClient.GetRow();
            }

            if (result != null)
            {
                player = new Player(result);
                playersByName.TryAdd(player.Username, player);

                return player;
            }

            return null;
        }

        public static PlayerController GetInstance()
        {
            if (instance == null)
                instance = new PlayerController();

            return instance;
        }
    }
}
