using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Players;

namespace AuroraEmu.Game
{
    public class Aurora
    {
        private static Aurora auroraInstance;

        public Aurora()
        {
            PlayerController.GetInstance();
            ClientManager.GetInstance();
            CatalogController.GetInstance();
        }

        public static Aurora GetInstance()
        {
            if (auroraInstance == null)
                auroraInstance = new Aurora();
            return auroraInstance;
        }
    }
}
