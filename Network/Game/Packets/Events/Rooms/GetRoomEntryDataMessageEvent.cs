using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetRoomEntryDataMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.QueueComposer(new UsersMessageComposer(client.LoadingRoom.Actors.Values));
            client.LoadingRoom.AddActor(client);
            client.CurrentRoom.SendComposer(new UsersMessageComposer(client.RoomActor));

            client.QueueComposer(new RoomEntryInfoMessageComposer(true, client.CurrentRoom.Id, true));
            client.QueueComposer(new GetGuestRoomResultComposer(client.CurrentRoom));

            client.QueueComposer(new ObjectsMessageComposer(client.CurrentRoom.GetFloorItems()));
            client.QueueComposer(new ItemsMessageComposer(client.CurrentRoom.GetWallItems()));

            if (client.CurrentRoom.OwnerId == client.Player.Id)
            {
                client.QueueComposer(new YouAreControllerMessageComposer());
                client.QueueComposer(new YouAreOwnerMessageComposer());
            }

            client.Flush();
        }
    }
}
