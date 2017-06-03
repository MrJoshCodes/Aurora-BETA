using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Events.Catalogue;
using AuroraEmu.Network.Game.Packets.Events.Handshake;
using AuroraEmu.Network.Game.Packets.Events.Messenger;
using AuroraEmu.Network.Game.Packets.Events.Navigator;
using AuroraEmu.Network.Game.Packets.Events.Users;
using DotNetty.Buffers;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets
{
    public class PacketHelper
    {
        private readonly Dictionary<int, IPacketEvent> packetEvents;
        private static PacketHelper packetHelperInstance;

        public PacketHelper()
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
                //{ 12, new MessengerInitMessageEvent() },
                { 41, new HabboSearchMessageEvent() },
                { 434, new MyRoomsSearchMessageEvent() },
                { 151, new GetUserFlatCatsMessageEvent() },
                { 387, new CanCreateRoomMessageEvent() }
            };

            Engine.Logger.Info($"Loaded {packetEvents.Count} packet events.");
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

        public static PacketHelper GetInstance()
        {
            if (packetHelperInstance == null)
                packetHelperInstance = new PacketHelper();
            return packetHelperInstance;
        }
    }
}
