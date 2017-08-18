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

namespace AuroraEmu.Game
{
    public class MainDi
    {
        private readonly IDependencyLocator _locator;
        public ICatalogController CatalogController { get; set; }
        public IClientController ClientController { get; set; } 
        public IItemController ItemController { get; set; }
        public IMessengerController MessengerController { get; set; }
        public INavigatorController NavigatorController { get; set; } 
        public IPlayerController PlayerController { get; set; }
        public IRoomController RoomController { get; set; }
        public ITaskController TaskController { get; set; }
        public IWordfilterController WorldfilterController { get; set; }
        public IDatabaseController DatabaseController { get; set; }
        public IPacketController PacketController { get; set; }
        public IConfigController ConfigController { get; set; }
        public IWordfilterController WordfilterController { get; set; }
        public IGameNetworkListener GameNetworkListener { get; set; }

        public ICatalogDao CatalogDao { get; set; }
        public IItemDao ItemDao { get; set; }
        public IMessengerDao MessengerDao { get; set; }
        public INavigatorDao NavigatorDao { get; set; }
        public IPlayerDao PlayerDao { get; set; }
        public IRoomDao RoomDao { get; set; }
        public IWordfilterDao WordfilterDao { get; set; }
        public IBadgesDao BadgesDao { get; set; }

        public MainDi(IDependencyLocator locator)
        {
            _locator = locator;
        }

        public void SetupControllers()
        {
            ConfigController = _locator.Resolve<IConfigController>();
            DatabaseController = _locator.Resolve<IDatabaseController>();
            PlayerController = _locator.Resolve<IPlayerController>();
            ItemController = _locator.Resolve<IItemController>();
            CatalogController = _locator.Resolve<ICatalogController>();
            NavigatorController = _locator.Resolve<INavigatorController>();
            MessengerController = _locator.Resolve<IMessengerController>();
            ClientController = _locator.Resolve<IClientController>();
            RoomController = _locator.Resolve<IRoomController>();
            TaskController = _locator.Resolve<ITaskController>();
            WorldfilterController = _locator.Resolve<IWordfilterController>();
            PacketController = _locator.Resolve<IPacketController>();
            WordfilterController = _locator.Resolve<IWordfilterController>();
            GameNetworkListener = _locator.Resolve<IGameNetworkListener>();
        }

        public void SetupDaos()
        {
            CatalogDao = _locator.Resolve<ICatalogDao>();
            ItemDao = _locator.Resolve<IItemDao>();
            MessengerDao = _locator.Resolve<IMessengerDao>();
            NavigatorDao = _locator.Resolve<INavigatorDao>();
            PlayerDao = _locator.Resolve<IPlayerDao>();
            RoomDao = _locator.Resolve<IRoomDao>();
            WordfilterDao = _locator.Resolve<IWordfilterDao>();
            BadgesDao = _locator.Resolve<IBadgesDao>();
        }
    }
}
