using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;

namespace AuroraEmu.Game.Items.Handlers
{
    public class SwitchHandler : IItemHandler
    {
        public void Handle(Item item, Client client)
        {
            if (!int.TryParse(item.Data, out var currentState)) currentState = 0;
            
            var newState = currentState >= item.Definition.InteractorCount ? 0 : currentState + 1;

            item.Data = newState.ToString();
            Engine.Locator.ItemController.Dao.UpdateItemData(item.Id, item.Data);
        }

        public void Trigger(Item item, int request = 0)
        {
        }
    }
}