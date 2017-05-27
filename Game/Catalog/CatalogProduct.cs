using AuroraEmu.Game.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceCoins { get; set; }
        public int PricePixels { get; set; }
        public int Amount { get; set; }
        public int PageId { get; set; }
        public Item Template { get; set; }

        public CatalogProduct(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = (string)row["name"];
            this.PriceCoins = (int)row["price_coins"];
            this.PricePixels = (int)row["price_pixels"];
            this.Amount = (int)row["amount"];
            this.PageId = (int)row["page_id"];
            this.Template = (Item)row["Template_id"];
        }
    }
}
