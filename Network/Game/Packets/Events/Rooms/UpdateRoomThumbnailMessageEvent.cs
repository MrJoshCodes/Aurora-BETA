using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class UpdateRoomThumbnailMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int roomId = msgEvent.ReadVL64();

            if (client.CurrentRoomId > 0)
            {
                Room room = Engine.MainDI.RoomController.GetRoom(client.CurrentRoomId);
                string icon = msgEvent.ToString();

                room.Icon = icon;
                room.Save(new[] { "icon" }, new object[] { icon });

                client.SendComposer(new RoomThumbnailUpdateResultComposer(roomId));
                client.SendComposer(new RoomInfoUpdatedComposer(roomId));
                client.SendComposer(new GetGuestRoomResultComposer(room));
            }
        }
    }
}
