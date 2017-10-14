using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetRoomSettingsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int roomId = msgEvent.ReadVL64();

            if (client.CurrentRoom != null)
            {
                client.SendComposer(new RoomSettingsDataComposer(client.CurrentRoom));
            }
        }
    }
}
