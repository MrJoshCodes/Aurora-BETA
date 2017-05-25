using AuroraEmu.Game.Items;

namespace AuroraEmu.Game.Catalog
{
    public class CatalogProduct
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int PriceCoins { get; set; }
        public virtual int PricePixels { get; set; }
        public virtual Item Template { get; set; }
        public virtual int Amount { get; set; }
        public virtual int PageId { get; set; }
    }
}
