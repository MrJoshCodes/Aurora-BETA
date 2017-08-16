using AuroraEmu.Config;
using AuroraEmu.Database;
using AuroraEmu.Database.DAO;
using AuroraEmu.DI;
using AuroraEmu.DI.Config;
using AuroraEmu.DI.Database;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.DI.Game;
using AuroraEmu.DI.Game.Catalog;
using AuroraEmu.DI.Game.Clients;
using AuroraEmu.DI.Game.Items;
using AuroraEmu.DI.Game.Messenger;
using AuroraEmu.DI.Game.Navigator;
using AuroraEmu.DI.Game.Players;
using AuroraEmu.DI.Game.Rooms;
using AuroraEmu.DI.Game.Wordfilter;
using AuroraEmu.DI.Locator;
using AuroraEmu.DI.Network.Game;
using AuroraEmu.DI.Network.Game.Packets;
using AuroraEmu.Game;
using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Players;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Game.Wordfilter;
using AuroraEmu.Network.Game;
using AuroraEmu.Network.Game.Packets;
using Autofac;
using log4net;

namespace AuroraEmu
{
    public class Engine
    {
        public static ILog Logger { get; private set; }
        public static IContainer Container { get; set; }
        public static MainDi MainDI { get; set; }

        static void Main(string[] args)
        {
            Logger = LogManager.GetLogger(typeof(Engine));
            

            ContainerBuilder();
            while (true) 
            {
                switch (System.Console.ReadLine())
                {
                    case "reload_packets":
                        MainDI.PacketController.LoadPackets();
                        break;
                }
            }
        }

        private static void ContainerBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<MainDi>();
            builder.RegisterType<DependencyLocator>().As<IDependencyLocator>();

            //Database Controller
            builder.RegisterType<DatabaseController>().As<IDatabaseController>();

            //Config controller
            builder.RegisterType<ConfigController>().As<IConfigController>();

            //Packet controller
            builder.RegisterType<PacketController>().As<IPacketController>();

            //Network listener
            builder.RegisterType<GameNetworkListener>().As<IGameNetworkListener>();

            //Main Controllers
            builder.RegisterType<CatalogController>().As<ICatalogController>();
            builder.RegisterType<ClientController>().As<IClientController>();
            builder.RegisterType<ItemController>().As<IItemController>();
            builder.RegisterType<MessengerController>().As<IMessengerController>();
            builder.RegisterType<NavigatorController>().As<INavigatorController>();
            builder.RegisterType<PlayerController>().As<IPlayerController>();
            builder.RegisterType<RoomController>().As<IRoomController>();
            builder.RegisterType<TaskController>().As<ITaskController>();
            builder.RegisterType<WordfilterController>().As<IWordfilterController>();

            //DAO's
            builder.RegisterType<CatalogDao>().As<ICatalogDao>();
            builder.RegisterType<ItemDao>().As<IItemDao>();
            builder.RegisterType<MessengerDao>().As<IMessengerDao>();
            builder.RegisterType<NavigatorDao>().As<INavigatorDao>();
            builder.RegisterType<PlayerDao>().As<IPlayerDao>();
            builder.RegisterType<RoomDao>().As<IRoomDao>();
            builder.RegisterType<WordfilterDao>().As<IWordfilterDao>();

            Container = builder.Build();
            MainDI = Container.Resolve<MainDi>();
            MainDI.SetupDaos();
            MainDI.SetupControllers();
        }
    }
}
