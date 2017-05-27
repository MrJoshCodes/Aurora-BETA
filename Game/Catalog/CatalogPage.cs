using System.Collections.Generic;
using System.Data;

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

        public CatalogPage(DataRow row)
        {
            Id = (int)row["id"];
            Name = (string)row["name"];
            IconColor = (int)row["icon_color"];
            IconImage = (int)row["icon_image"];
            Development = (bool)row["in_development"];
            Visible = (bool)row["is_visible"];
            ParentId = (int)row["parent_id"];
            MinRank = (int)row["min_rank"];
            Layout = (string)row["layout"];
            HasContent = (bool)row["has_content"];
            Data = new Dictionary<string, List<string>> { { "image", new List<string>() }, { "text", new List<string>() } };
        }
    }
}
