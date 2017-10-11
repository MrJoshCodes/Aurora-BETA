using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models.Dimmer;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using System.Linq;

namespace AuroraEmu.Network.Game.Packets.Events.Items
{
    public class RoomDimmerGetPresetsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int itemId = client.CurrentRoom.GetWallItems().Where(x => x.Definition.HandleType == AuroraEmu.Game.Items.Handlers.HandleType.DIMMER).ToList()[0].Id;
            DimmerData data = Engine.MainDI.ItemController.GetDimmerData(itemId);
            client.SendComposer(new RoomDimmerPresetsMessageComposer(data));
        }
    }
}
