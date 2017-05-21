using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Events.Catalogue;
using AuroraEmu.Network.Game.Packets.Events.Handshake;
using AuroraEmu.Network.Game.Packets.Events.Users;
using DotNetty.Buffers;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets
{
    public class PacketHelper
    {
        private readonly Dictionary<int, IPacketEvent> packetEvents;
        private readonly Dictionary<int, string> packetNames;
        private static PacketHelper packetHelperInstance;

        public PacketHelper()
        {
            packetEvents = new Dictionary<int, IPacketEvent>
            {
                { 8, new GetCreditsMessageEvent() },
                { 101, new GetCatalogIndexMessageEvent() },
                { 102, new GetCatalogPageMessageEvent() },
                { 206, new InitCryptoMessageEvent() },
                { 415, new SSOTicketMessageEvent() }
            };

            packetNames = new Dictionary<int, string>
            {
                { 8, "GetCreditsMessageEvent" },
                { 101, "GetCatalogIndexMessageEvent" },
                { 102, "GetCatalogPageMessageEvent" },
                { 206, "InitCryptoMessageEvent" },
                { 415, "SSOTicketMessageEvent" }
            };

            Engine.Logger.Info($"Loaded {packetEvents.Count} packet events.");
        }

        public void Handle(Client client, IByteBuffer buffer)
        {
            IPacketEvent packetEvent;

            MessageEvent msgEvent = new MessageEvent(buffer);

            if (packetEvents.TryGetValue(msgEvent.HeaderId, out packetEvent))
            {
                if (packetNames.ContainsKey(msgEvent.HeaderId))
                {
                    Engine.Logger.Info($"Handled incoming packet: {packetNames[msgEvent.HeaderId]}");
                }
                packetEvent.Run(client, msgEvent);
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
