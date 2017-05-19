using AuroraEmu.Database;
using AuroraEmu.Network.Game;
using log4net;

namespace AuroraEmu
{
    public class Engine
    {
        public static ILog Logger { get; private set; }

        public static DatabaseHelper Database { get; private set; }
        public static Game.Game Game { get; private set; }
        public static GameNetworkListener GameNetwork { get; private set; }

        static void Main(string[] args)
        {
            Logger = LogManager.GetLogger(typeof(Engine));

            Database = new DatabaseHelper();
            Game = new Game.Game();

            GameNetwork = new GameNetworkListener();

            System.Console.ReadLine();
        }
    }
}
