using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Wordfilter;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class ChatMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            string input = msgEvent.ReadString();
            client.CurrentRoom.SendComposer(new ChatMessageComposer(client.RoomActor.VirtualID, WordfilterController.GetInstance().CheckString(input)));
        }
    }
}
