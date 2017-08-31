using AuroraEmu.Game.Items;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Catalog
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
