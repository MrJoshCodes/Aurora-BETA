using System.Collections.Generic;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogPage
    {
        private IDictionary<string, IList<CatalogPageData>> _pageData;

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int IconColor { get; set; }
        public virtual int IconImage { get; set; }
        public virtual bool Development { get; set; }
        public virtual bool Visible { get; set; }
        public virtual int ParentId { get; set; }
        public virtual int MinRank { get; set; }
        public virtual string Layout { get; set; }
        public virtual bool HasContent { get; set; }
        public virtual IDictionary<string, IList<CatalogPageData>> PageData
        {
            get
            {
                if (_pageData == null)
                {
                    _pageData = new Dictionary<string, IList<CatalogPageData>>
                    {
                        { "image", new List<CatalogPageData>() },
                        { "text", new List<CatalogPageData>() }
                   };
                }

                return _pageData;
            }
        }
    }
}
