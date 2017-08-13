using AuroraEmu.Config;
using AuroraEmu.Database;
using AuroraEmu.Game;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Players;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Game.Tasks;
using AuroraEmu.Game.Wordfilter;
using AuroraEmu.Network.Game;
using AuroraEmu.Network.Game.Packets;
using log4net;

namespace AuroraEmu
{
    public class Engine
    {
        public static ILog Logger { get; private set; }

        static void Main(string[] args)
        {
            Logger = LogManager.GetLogger(typeof(Engine));

            ConfigLoader.GetInstance();

            DatabaseManager.GetInstance();

            PlayerController.GetInstance();
            ItemController.GetInstance();
            CatalogController.GetInstance();
            NavigatorController.GetInstance();
            MessengerController.GetInstance();
            RoomController.GetInstance().LoadRoomMaps();
            WordfilterController.GetInstance();

            GameNetworkListener.GetInstance();
            while (true) 
            {
                switch (System.Console.ReadLine())
                {
                    case "reload_packets":
                        PacketHelper.GetInstance().LoadPackets();
                        break;
                }
            }
        }

        public static bool EnumToBool(string Enum)
        {
            if (Enum == "1")
            {
                return true;
            }
            return false;
        }
    }
}
