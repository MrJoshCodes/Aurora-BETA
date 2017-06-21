using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class UpdateRoomThumbnailMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int roomId = msgEvent.ReadVL64();

            if (client.CurrentRoom != null)
            {
                string icon = msgEvent.ToString();

                client.CurrentRoom.Icon = icon;
                client.CurrentRoom.Save(new[] { "icon" }, new object[] { icon });

                client.SendComposer(new RoomThumbnailUpdateResultComposer(roomId));
                client.SendComposer(new RoomInfoUpdatedComposer(roomId));
                client.SendComposer(new GetGuestRoomResultComposer(client.CurrentRoom));
            }
        }
    }
}
