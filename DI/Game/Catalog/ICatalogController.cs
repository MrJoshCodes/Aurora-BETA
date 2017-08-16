using AuroraEmu.Game.Catalog;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Catalog
{
    public interface ICatalogController
    {
        void ReloadPages();

        string GenerateExtraData(CatalogProduct product, string extraData);

        List<CatalogDealItem> GetDeal(int dealId);

        void ReloadProducts();

        void ReloadDeals();

        CatalogPage GetPage(int id);

        CatalogProduct GetProduct(int id);

        List<CatalogPage> GetPages(int parent);

        List<CatalogProduct> GetProducts(int pageId);
    }
}
