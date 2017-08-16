using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetRoomSettingsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int roomId = msgEvent.ReadVL64();
            Room room = Engine.MainDI.RoomController.GetRoom(roomId);

            if (room != null)
            {
                client.SendComposer(new RoomSettingsDataComposer(room));
            }
        }
    }
}
