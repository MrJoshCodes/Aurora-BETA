using AuroraEmu.Game.Items;
using System.Data;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogDealItem
    {
        private Item template;

        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int Amount { get; set; }

        public CatalogDealItem(DataRow row)
        {
            Id = (int)row["id"];
            TemplateId = (int)row["template_id"];
            Amount = (int)row["amount"];
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
