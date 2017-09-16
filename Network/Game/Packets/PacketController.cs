using AuroraEmu.DI.Network.Game.Packets;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Events.Catalogue;
using AuroraEmu.Network.Game.Packets.Events.Handshake;
using AuroraEmu.Network.Game.Packets.Events.Inventory;
using AuroraEmu.Network.Game.Packets.Events.Inventory.Badges;
using AuroraEmu.Network.Game.Packets.Events.Messenger;
using AuroraEmu.Network.Game.Packets.Events.Navigator;
using AuroraEmu.Network.Game.Packets.Events.Rooms;
using AuroraEmu.Network.Game.Packets.Events.Users;
using AuroraEmu.Network.Game.Packets.Events.Users.Clothing;
using DotNetty.Buffers;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets
{
    public class PacketController : IPacketController
    {
        private Dictionary<int, IPacketEvent> packetEvents;

        public PacketController()
        {
            LoadPackets();

            Engine.Logger.Info($"Loaded {packetEvents.Count} packet events.");
        }

        public void LoadPackets()
        {
            packetEvents = new Dictionary<int, IPacketEvent>
            {
                { 7, new InfoRetrieveMessageEvent() },
                { 8, new GetCreditsMessageEvent() },
                { 101, new GetCatalogIndexMessageEvent() },
                { 102, new GetCatalogPageMessageEvent() },
                { 380, new GetOfficialRoomsMessageEvent() },
                { 206, new InitCryptoMessageEvent() },
                { 415, new SSOTicketMessageEvent() },

                { 12, new MessengerInitMessageEvent() },
                { 33, new SendMsgMessageEvent() },
                { 39, new RequestBuddyMessageEvent() },
                { 15, new FriendListUpdateMessageEvent() },
                { 37, new AcceptBuddyMessageEvent() },
                { 38, new DeclineBuddyMessageEvent() },
                { 40, new RemoveBuddyMessageEvent() },

                {2, new OpenConnectionMessageEvent()},
                { 41, new HabboSearchMessageEvent() },
                { 434, new MyRoomsSearchMessageEvent() },
                {432, new MyFriendsRoomsSearchMessageEvent()},
                { 151, new GetUserFlatCatsMessageEvent() },
                { 387, new CanCreateRoomMessageEvent() },
                { 29, new CreateFlatMessageEvent() },
                { 391, new OpenFlatConnectionMessageEvent() },
                { 230, new GetHabboGroupBadgesMessageEvent() },
                { 215, new GetFurnitureAliasesMessageEvent() },
                { 60, new GetHeightMapMessageEvent() },
                { 390, new GetRoomEntryDataMessageEvent() },
                { 126, new GetRoomAdMessageEvent() },
                { 382, new GetPopularRoomTagsMessageEvent() },
                { 52, new ChatMessageEvent() },
                { 404, new RequestFurniInventoryEvent() },
                { 100, new PurchaseFromCatalogEvent() },
                { 437, new RoomTextSearchMessageEvent() },
                { 400, new GetRoomSettingsMessageEvent() },
                { 386, new UpdateRoomThumbnailMessageEvent() },
                { 75, new MoveAvatarMessageEvent() },
                { 94, new WaveMessageEvent() },

                { 157, new GetBadgesEvent() },
                { 158, new SetActivatedBadgesEvent() },
                { 67, new PickupObjectMessageEvent() },
                { 90, new PlaceObjectMessageEvent() },
                { 129, new RedeemVoucherMessageEvent() },
                { 233, new GetBuddyRequestsMessageEvent() },
                { 73, new MoveObjectMessageEvent() },
                { 44, new UpdateFigureDataMessageEvent() },
                { 26, new ScrGetUserInfoMessageEvent() }
            };
        }

        public void Handle(Client client, IByteBuffer buffer)
        {
            MessageEvent msgEvent = new MessageEvent(buffer);

            if (packetEvents.TryGetValue(msgEvent.HeaderId, out IPacketEvent packetEvent))
            {
                packetEvent.Run(client, msgEvent);
                Engine.Logger.Info($"Handled incoming packet {packetEvent.GetType().Name}.");
            }
            else
            {
                Engine.Logger.Warn($"Unregistered packet event #{msgEvent.HeaderId}: {msgEvent.ToString()}");
            }
        }
    }
}
