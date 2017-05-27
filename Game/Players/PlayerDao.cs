using AuroraEmu.Database;
using System.Collections.Concurrent;
using System.Data;

namespace AuroraEmu.Game.Players
{
    public class PlayerDao
    {
        private static ConcurrentDictionary<int, Player> playersById;

        static PlayerDao()
        {
            playersById = new ConcurrentDictionary<int, Player>();
        }

        public static Player GetPlayerById(int id)
        {
            Player player;

            if (playersById.TryGetValue(id, out player))
                return player;

            DataRow result = null;

            using (DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.WriteQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, sso_ticket FROM players WHERE id = @id;");
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

            if (result != null)
            {
                Player player = new Player(result);
                playersById.AddOrUpdate(player.Id, player, (oldkey, oldvalue) => player);
                return player;
            }

            return null;
        }
    }
}
