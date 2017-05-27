using AuroraEmu.Game.Items;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogProduct
    {
        private Item template;
        private List<CatalogDealItem> dealItems;

        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceCoins { get; set; }
        public int PricePixels { get; set; }
        public int Amount { get; set; }
        public int PageId { get; set; }
        public int TemplateId { get; set; }
        public bool IsDeal { get; set; }
        public int DealId { get; set; }

        public CatalogProduct(DataRow row)
        {
            Id = (int)row["id"];
            Name = (string)row["name"];
            PriceCoins = (int)row["price_coins"];
            PricePixels = (int)row["price_pixels"];
            PageId = (int)row["page_id"];
            IsDeal = (bool)row["is_deal"];
            Amount = IsDeal ? -1 : (int)row["amount"];
            TemplateId = IsDeal ? -1 : (int)row["Template_id"];
            DealId = IsDeal ? (int)row["deal_id"] : -1;
        }

        public List<CatalogDealItem> DealItems
        {
            get
            {
                if (dealItems == null)
                    dealItems = CatalogController.GetInstance().GetDeal(DealId);

                return dealItems;
            }
        }

        public Item Template
        {
            get
            {
                if (template == null)
                    template = ItemController.GetInstance().GetTemplate(TemplateId);

                return template;
            }
        }
    }
}
