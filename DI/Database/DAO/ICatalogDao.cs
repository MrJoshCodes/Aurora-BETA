using AuroraEmu.Game.Catalog;
using AuroraEmu.Game.Catalog.Voucher;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface ICatalogDao
    {
        Dictionary<int, CatalogPage> ReloadCatalogPage(Dictionary<int, CatalogPage> pages);

        Dictionary<int, CatalogProduct> ReloadProducts(Dictionary<int, CatalogProduct> products);

        Dictionary<int, List<CatalogDealItem>> ReloadDeals(Dictionary<int, List<CatalogDealItem>> deals);

        Dictionary<string, Voucher> ReloadVouchers(Dictionary<string, Voucher> vouchers);
    }
}
