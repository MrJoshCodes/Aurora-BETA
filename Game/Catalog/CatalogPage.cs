using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IconColor { get; set; }
        public int IconImage { get; set; }
        public bool Development { get; set; }
        public bool Visible { get; set; }
        public int ParentId { get; set; }
        public int MinRank { get; set; }
        public string Layout { get; set; }
        public bool HasContent { get; set; }

        public Dictionary<string, List<string>> Data { get; set; }

        public CatalogPage(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("name");
            IconColor = reader.GetInt32("icon_color");
            IconImage = reader.GetInt32("icon_image");
            Development = reader.GetBoolean("in_development");
            Visible = reader.GetBoolean("is_visible");
            ParentId = reader.GetInt32("parent_id");
            MinRank = reader.GetInt32("min_rank");
            Layout = reader.GetString("layout");
            HasContent = reader.GetBoolean("has_content");
            Data = new Dictionary<string, List<string>> { { "image", new List<string>() }, { "text", new List<string>() } };
        }
    }
}
