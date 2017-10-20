using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Network.Game.Packets.Composers.Items;

namespace AuroraEmu.Network.Game.Packets.Events.Items
{
    public class UseFurnitureMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int itemId = msgEvent.ReadVL64();

            if (Engine.Locator.RoomController.GetRoom(client.CurrentRoomId).Items.TryGetValue(itemId, out Item item))
            {
                if (item.Definition.MaxInteractionState == 0) return;
                
                int nextState;

                if (!int.TryParse(item.Data, out int currentState) || currentState < 0 ||
                    currentState >= item.Definition.MaxInteractionState)
                {
                    nextState = 0;
                }
                else
                {
                    nextState = currentState + 1;
                }

                item.Data = nextState.ToString();
                client.CurrentRoom.SendComposer(new ObjectDataUpdateMessageComposer(itemId, item.Data));
            }
        }
    }
}