using AuroraEmu.Game.Catalog.Models;
using AuroraEmu.Game.Catalog.Vouchers;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface ICatalogDao
    {
        void ReloadCatalogPage(Dictionary<int, CatalogPage> pages);

        void ReloadProducts(Dictionary<int, CatalogProduct> products);

        void ReloadDeals(Dictionary<int, List<CatalogDealItem>> deals);

        void ReloadVouchers(Dictionary<string, Voucher> vouchers);
    }
}
