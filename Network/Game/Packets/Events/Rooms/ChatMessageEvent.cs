using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class ChatMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            string input = msgEvent.ReadString();
            client.CurrentRoom.SendComposer(new ChatMessageComposer(((RoomActor) client.UserActor).VirtualId, Engine.MainDI.WorldfilterController.CheckString(input)));
        }
    }
}
