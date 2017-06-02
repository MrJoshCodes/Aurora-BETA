using AuroraEmu.Database;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Players
{
    public class PlayerController
    {
        private static PlayerController instance;

        private ConcurrentDictionary<int, Player> playersById;

        public PlayerController()
        {
            playersById = new ConcurrentDictionary<int, Player>();
        }

        public bool PlayerIsOnline(int playerId)
        {
            Player player = null;
            return playersById.TryGetValue(playerId, out player);
        }

        public Player GetPlayerById(int id)
        {
            Player player;

            if (playersById.TryGetValue(id, out player))
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
