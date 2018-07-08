using AuroraEmu.Game.Clients;
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
                string icon = msgEvent.ToString();

                client.CurrentRoom.Icon = icon;
                client.CurrentRoom.Save(("icon", icon));

                client.SendComposer(new RoomThumbnailUpdateResultComposer(roomId));
                client.SendComposer(new RoomInfoUpdatedComposer(roomId));
                client.SendComposer(new GetGuestRoomResultComposer(client.CurrentRoom));
            }
        }
    }
}
