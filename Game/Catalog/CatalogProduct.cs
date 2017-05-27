using AuroraEmu.Game.Items;
using System.Data;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogProduct
    {
        private Item template;

        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceCoins { get; set; }
        public int PricePixels { get; set; }
        public int Amount { get; set; }
        public int PageId { get; set; }
        public int TemplateId { get; set; }

        public CatalogProduct(DataRow row)
        {
            Id = (int)row["id"];
            Name = (string)row["name"];
            PriceCoins = (int)row["price_coins"];
            PricePixels = (int)row["price_pixels"];
            Amount = (int)row["amount"];
            PageId = (int)row["page_id"];
            TemplateId = (int)row["Template_id"];
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
