using System.Data;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogPageData
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        
        public CatalogPageData(DataRow row)
        {
            Id = (int)row["id"];
            PageId = (int)row["page_id"];
            Type = (string)row["type"];
            Value = (string)row["value"];
        }
    }
}
