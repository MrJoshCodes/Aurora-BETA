using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class ChatMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.CurrentRoomId < 1)
                return;
            
            string input = msgEvent.ReadString();

            if (input.StartsWith(":") && Engine.Locator.CommandController.TryHandleCommand(client, input.Substring(1)))
                return;

            client.CurrentRoom.SendComposer(new ChatMessageComposer(client.UserActor.VirtualId, Engine.Locator.WorldfilterController.CheckString(input)));
        }
    }
}
