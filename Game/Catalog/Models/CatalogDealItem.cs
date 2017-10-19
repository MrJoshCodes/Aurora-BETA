using AuroraEmu.Game.Items.Models;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Catalog.Models
{
    public class CatalogDealItem
    {
        private ItemDefinition _template;

        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int Amount { get; set; }

        public CatalogDealItem(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            TemplateId = reader.GetInt32("template_id");
            Amount = reader.GetInt32("amount");
        }

        public ItemDefinition Template =>
            _template ?? (Engine.Locator.ItemController.GetTemplate(TemplateId));
    }
}
