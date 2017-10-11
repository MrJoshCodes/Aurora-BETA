using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Catalog.Models
{
    public class CatalogPageData
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        
        public CatalogPageData(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            PageId = reader.GetInt32("page_id");
            Type = reader.GetString("type");
            Value = reader.GetString("value");
        }
    }
}
