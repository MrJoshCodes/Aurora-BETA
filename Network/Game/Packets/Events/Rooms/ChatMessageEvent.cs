using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class ChatMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.CurrentRoomId < 1)
                return;

            Room room = Engine.MainDI.RoomController.GetRoom(client.CurrentRoomId);
            string input = msgEvent.ReadString();
            room.SendComposer(new ChatMessageComposer(((RoomActor) client.UserActor).VirtualId, Engine.MainDI.WorldfilterController.CheckString(input)));
        }
    }
}
