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

        public PacketHelper()
        {
            packetEvents = new Dictionary<int, IPacketEvent>
            {
                { 8, new GetCreditsMessageEvent() },
                { 101, new GetCatalogIndexMessageEvent() },
                { 206, new InitCryptoMessageEvent() },
                { 415, new SSOTicketMessageEvent() }
            };

            Engine.Logger.Info($"Loaded {packetEvents.Count} packet events.");
        }

        public void Handle(Client client, IByteBuffer buffer)
        {
            IPacketEvent packetEvent;

            MessageEvent msgEvent = new MessageEvent(buffer);

            if (packetEvents.TryGetValue(msgEvent.HeaderId, out packetEvent))
            {
                packetEvent.Run(client, msgEvent);
            }
            else
            {
                Engine.Logger.Warn($"Unregistered packet event #{msgEvent.HeaderId}: {msgEvent.ToString()}");
            }
        }
    }
}
