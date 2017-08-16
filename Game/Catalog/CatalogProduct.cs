using AuroraEmu.Game.Items;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogProduct
    {
        private ItemDefinition _template;
        private List<CatalogDealItem> _dealItems;

        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceCoins { get; set; }
        public int PricePixels { get; set; }
        public int Amount { get; set; }
        public int PageId { get; set; }
        public int TemplateId { get; set; }
        public bool IsDeal { get; set; }
        public int DealId { get; set; }
        public string Data { get; set; }

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
            Data = (string)row["data"];
        }

        public List<CatalogDealItem> DealItems
        {
            get
            {
                if (_dealItems == null)
                    _dealItems = Engine.MainDI.CatalogController.GetDeal(DealId);

                return _dealItems;
            }
        }

        public ItemDefinition Template
        {
            get
            {
                if (_template == null)
                    _template = Engine.MainDI.ItemController.GetTemplate(TemplateId);

                return _template;
            }
        }
    }
}
