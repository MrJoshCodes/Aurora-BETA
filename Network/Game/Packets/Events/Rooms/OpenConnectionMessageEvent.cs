using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    public class OpenConnectionMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            msgEvent.ReadVL64();
            int id = msgEvent.ReadVL64();
            msgEvent.ReadVL64();

            Room room = Engine.Locator.RoomController.GetRoom(id);

            if (room == null) return;

            if (client.CurrentRoomId != 0 &&
                Engine.Locator.RoomController.Rooms.TryGetValue(client.CurrentRoomId, out Room currentRoom))
                currentRoom.RemoveActor(client.UserActor, false);

            client.LoadingRoomId = id;
            
            MessageComposer fuseResponse = new MessageComposer(166);
            fuseResponse.AppendString("/client/public/" + room.Model + "/0", 0);
            client.SendComposer(fuseResponse);

            client.SendComposer(new RoomReadyMessageComposer(id, room.Model));
        }
    }
}