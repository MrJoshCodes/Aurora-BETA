using AuroraEmu.Game.Items.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AuroraEmu.Game.Catalog.Models
{
    public class CatalogProduct
    {
        private ItemDefinition _template;
        private List<CatalogDealItem> _dealItems;

        public int Id { get; set; }
        public int PriceCoins { get; set; }
        public int PricePixels { get; set; }
        public int Amount { get; set; }
        public int PageId { get; set; }
        public int TemplateId { get; set; }
        public int DealId { get; set; }
        public int Order { get; set; }
        public bool IsDeal { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }

        public CatalogProduct(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("name");
            PriceCoins = reader.GetInt32("price_coins");
            PricePixels = reader.GetInt32("price_pixels");
            PageId = reader.IsDBNull(reader.GetOrdinal("page_id")) ? 0 : reader.GetInt32("page_id");
            IsDeal = reader.GetBoolean("is_deal");
            Amount = IsDeal ? -1 : reader.GetInt32("amount");
            TemplateId = IsDeal ? -1 : reader.GetInt32("Template_id");
            DealId = IsDeal ? reader.GetInt32("deal_id") : -1;
            Data = reader.GetString("data");
            Order = reader.GetInt32("order");
        }

        public List<CatalogDealItem> DealItems =>
            _dealItems ?? (Engine.Locator.CatalogController.GetDeal(DealId));

        public ItemDefinition Template =>
            _template ?? (Engine.Locator.ItemController.GetTemplate(TemplateId));
    }
}
