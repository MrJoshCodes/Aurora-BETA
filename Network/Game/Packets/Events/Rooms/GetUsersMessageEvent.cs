using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetUsersMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.LoadingRoomId < 1)
                return;

            Room room = Engine.Locator.RoomController.GetRoom(client.LoadingRoomId);

            client.QueueComposer(new UsersMessageComposer(room.Actors.Values));
            room.AddUserActor(client);
            client.CurrentRoom.SendComposer(new UsersMessageComposer(client.UserActor));
        }
    }
}
