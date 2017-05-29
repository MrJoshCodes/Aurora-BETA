using AuroraEmu.Database;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game;
using log4net;

namespace AuroraEmu
{
    public class Engine
    {
        public static ILog Logger { get; private set; }

        static void Main(string[] args)
        {
            Logger = LogManager.GetLogger(typeof(Engine));

            DatabaseManager.GetInstance();

            PlayerController.GetInstance();
            ItemController.GetInstance();
            CatalogController.GetInstance();
            NavigatorController.GetInstance();
            MessengerController.GetInstance();

            GameNetworkListener.GetInstance();

            System.Console.ReadLine();
        }
    }
}
