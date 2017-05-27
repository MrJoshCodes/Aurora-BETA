using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.Id = (int)row["id"];
            this.PageId = (int)row["page_id"];
            this.Type = (string)row["type"];
            this.Value = (string)row["value"];
        }
    }
}
