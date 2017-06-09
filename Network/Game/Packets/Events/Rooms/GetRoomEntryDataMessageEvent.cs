using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class GetRoomEntryDataMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new UsersMessageComposer(client.LoadingRoom.Actors.Values));
            client.LoadingRoom.AddActor(client);
            client.CurrentRoom.SendComposer(new UsersMessageComposer(client.RoomActor));

            client.SendComposer(new RoomEntryInfoMessageComposer(true, client.CurrentRoom.Id, true));
            client.SendComposer(new GetGuestRoomResultComposer(client.CurrentRoom));
        }
    }
}
