using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Misc;
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

            if (room.Map == null)
            {
                client.LoadingRoomId = 0;
                client.QueueComposer(new CloseConnectionMessageComposer());
                client.QueueComposer(new HabboBroadcastMessageComposer($"Model {room.Model} not found!"));
                client.Flush();

                return;
            }

            new GetHeightMapMessageEvent().Run(client, null);
            
            client.QueueComposer(new UsersMessageComposer(room.Actors.Values));
            room.AddUserActor(client);
            client.CurrentRoom.SendComposer(new UsersMessageComposer(client.UserActor));

            if (room.IsFrontpageItem && room.FrontpageItem.ExternalText.Length > 0) 
            {
                client.QueueComposer(new RoomEntryInfoMessageComposer(room.FrontpageItem.ExternalText));
                client.QueueComposer(new SpecialItemsMessageComposer(client.CurrentRoom.GetItems()));
            } 
            else
            {
                client.QueueComposer(new RoomEntryInfoMessageComposer(true, client.CurrentRoom.Id, true));
                client.QueueComposer(new GetGuestRoomResultComposer(client.CurrentRoom));
            }

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
