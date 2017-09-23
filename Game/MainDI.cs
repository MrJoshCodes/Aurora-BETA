using AuroraEmu.DI.Config;
using AuroraEmu.DI.Database;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.DI.Game;
using AuroraEmu.DI.Game.Catalog;
using AuroraEmu.DI.Game.Clients;
using AuroraEmu.DI.Game.Commands;
using AuroraEmu.DI.Game.Items;
using AuroraEmu.DI.Game.Messenger;
using AuroraEmu.DI.Game.Navigator;
using AuroraEmu.DI.Game.Players;
using AuroraEmu.DI.Game.Rooms;
using AuroraEmu.DI.Game.Subscription;
using AuroraEmu.DI.Game.Wordfilter;
using AuroraEmu.DI.Locator;
using AuroraEmu.DI.Network.Game;
using AuroraEmu.DI.Network.Game.Packets;

namespace AuroraEmu.Game
{
    public class MainDi
    {
        private readonly IDependencyLocator _locator;
        public ICatalogController CatalogController { get; private set; }
        public IClientController ClientController { get; private set; } 
        public IItemController ItemController { get; private set; }
        public IMessengerController MessengerController { get; private set; }
        public INavigatorController NavigatorController { get; private set; } 
        public IPlayerController PlayerController { get; private set; }
        public IRoomController RoomController { get; private set; }
        public ITaskController TaskController { get; private set; }
        public IWordfilterController WorldfilterController { get; private set; }
        public IConnectionPool ConnectionPool { get; private set; }
        public IPacketController PacketController { get; private set; }
        public IConfigController ConfigController { get; private set; }
        public IWordfilterController WordfilterController { get; private set; }
        public ICommandController CommandController { get; private set; }
        public IGameNetworkListener GameNetworkListener { get; private set; }
        public ISubscriptionController SubscriptionController { get; private set; }

        public ICatalogDao CatalogDao { get; private set; }
        public IItemDao ItemDao { get; private set; }
        public IMessengerDao MessengerDao { get; private set; }
        public INavigatorDao NavigatorDao { get; private set; }
        public IPlayerDao PlayerDao { get; private set; }
        public IRoomDao RoomDao { get; private set; }
        public IWordfilterDao WordfilterDao { get; private set; }
        public IBadgesDao BadgesDao { get; private set; }
        public ISubscriptionDao SubscriptionDao { get; private set; }

        public MainDi(IDependencyLocator locator)
        {
            _locator = locator;
        }

        public void SetupControllers()
        {
            ConfigController = _locator.Resolve<IConfigController>();
            ConnectionPool = _locator.Resolve<IConnectionPool>();
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
            CommandController = _locator.Resolve<ICommandController>();
            GameNetworkListener = _locator.Resolve<IGameNetworkListener>();
            SubscriptionController = _locator.Resolve<ISubscriptionController>();
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
            SubscriptionDao = _locator.Resolve<ISubscriptionDao>();
        }
    }
}
