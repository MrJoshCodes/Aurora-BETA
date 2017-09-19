using AuroraEmu.Game.Clients;

namespace AuroraEmu.Game.Items
{
    public interface IItemHandler
    {
        void Handle(Item item, Client client);

        void Trigger(Item item, int request = 0);
    }
}
