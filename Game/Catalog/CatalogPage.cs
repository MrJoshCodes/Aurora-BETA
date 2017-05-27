using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public CatalogPage(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = (string)row["name"];
            this.IconColor = (int)row["icon_color"];
            this.IconImage = (int)row["icon_image"];
            this.Development = (bool)row["in_development"];
            this.Visible = (bool)row["is_visible"];
            this.ParentId = (int)row["parent_id"];
            this.MinRank = (int)row["min_rank"];
            this.Layout = (string)row["layout"];
            this.HasContent = (bool)row["has_content"];
        }
    }
}
