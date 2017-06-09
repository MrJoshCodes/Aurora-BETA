using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class OpenFlatConnectionMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int roomId = msgEvent.ReadVL64();
            string password = msgEvent.ReadString();

            Room room = RoomController.GetInstance().GetRoom(roomId);

            if (room == null)
                return;

            client.QueueComposer(new OpenConnectionMessageComposer());

            // Now, here comes the stuff later which checks stuff

            client.QueueComposer(new RoomReadyMessageComposer(room.Id, room.Model));

            client.Flush();

            client.LoadingRoom = room; 
        }
    }
}
