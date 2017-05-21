using AuroraEmu.Database;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Concurrent;

namespace AuroraEmu.Game.Players
{
    public class PlayerController
    {
        private readonly ConcurrentDictionary<int, Player> players;
        private static PlayerController playerControllerInstance;

        public PlayerController()
        {
            players = new ConcurrentDictionary<int, Players.Player>();
        }

        public Player this[int id]
        {
            get
            {
                return GetPlayerById(id);
            }
        }

        private Player GetPlayerById(int id)
        {
            Player player;

            if (players.TryGetValue(id, out player))
                return players[id];

            using (ISession session = DatabaseHelper.GetInstance().SessionFactory.OpenSession())
            {
                player = session.Get<Player>(id);
            }

            if (player != null)
            {
                players.TryAdd(player.Id, player);
            }

            return player;
        }

        public Player GetPlayerBySSO(string sso)
        {
            Player player;

            using (ISession session = DatabaseHelper.GetInstance().SessionFactory.OpenSession())
            {
                player = session.CreateCriteria<Player>().Add(Restrictions.Eq("SSO", sso)).UniqueResult<Player>();
            }

            if (player != null)
            {
                players.AddOrUpdate(player.Id, player, ((id, plr) => player));
            }

            return player;
        }

        public static PlayerController GetInstance()
        {
            if (playerControllerInstance == null)
                playerControllerInstance = new PlayerController();
            return playerControllerInstance;
        }
    }
}
