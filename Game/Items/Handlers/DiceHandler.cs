using AuroraEmu.Game.Clients;
using System;

namespace AuroraEmu.Game.Items.Handlers
{
    public class DiceHandler : IItemHandler
    {
        public void Handle(Item item, Client client)
        {
            item.Data = new Random().Next(1, 6).ToString();
        }

        public void Trigger(Item item, int request = 0)
        {
            if(request == -1)
            {
                item.Data = "-1";
            }
            else
            {
                item.Data = "0";
            }
        }
    }
}
