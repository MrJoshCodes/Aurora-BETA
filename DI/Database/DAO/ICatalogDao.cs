using AuroraEmu.Game.Catalog;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.DI.Database.DAO
{
    public interface ICatalogDao
    {
        Dictionary<int, CatalogPage> ReloadCatalogPage(Dictionary<int, CatalogPage> pages);

        DataTable ReloadProducts();

        DataTable ReloadDeals();
    }
}
