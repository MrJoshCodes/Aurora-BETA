using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Players;

namespace AuroraEmu.Game
{
    public class Game
    {
        public PlayerController Players { get; private set; }
        public ClientManager Clients { get; private set; }
        public CatalogController Catalog { get; private set; }

        public Game()
        {
            Players = new PlayerController();
            Clients = new ClientManager();
            Catalog = new CatalogController();
        }
    }
}
