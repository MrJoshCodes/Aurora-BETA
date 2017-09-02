using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetRoomEntryDataMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.LoadingRoomId < 1)
                return;

            Room room = Engine.MainDI.RoomController.GetRoom(client.LoadingRoomId);

            new GetHeightMapMessageEvent().Run(client, null);

            client.QueueComposer(new UsersMessageComposer(room.Actors.Values));
            room.AddUserActor(client);
            room.SendComposer(new UsersMessageComposer(client.UserActor));

            client.QueueComposer(new RoomEntryInfoMessageComposer(true, room.Id, true));
            client.QueueComposer(new GetGuestRoomResultComposer(room));

            client.QueueComposer(new ObjectsMessageComposer(room.GetFloorItems()));
            client.QueueComposer(new ItemsMessageComposer(room.GetWallItems()));

            if (room.OwnerId == client.Player.Id)
            {
                client.QueueComposer(new YouAreControllerMessageComposer());
                client.QueueComposer(new YouAreOwnerMessageComposer());
            }

            client.Flush();
        }
    }
}
