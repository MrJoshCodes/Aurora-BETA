using AuroraEmu.Game.Catalog;
using FluentNHibernate.Mapping;

namespace AuroraEmu.Database.Mappings
{
    public class CatalogProductMap : ClassMap<CatalogProduct>
    {
        public CatalogProductMap()
        {
            Table("catalog_products");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Name).Column("name").Length(30).Not.Nullable();
            Map(x => x.PriceCoins).Column("price_coins").Not.Nullable();
            Map(x => x.PricePixels).Column("price_pixels").Not.Nullable();
            References(x => x.Template);
            Map(x => x.Amount).Column("amount").Not.Nullable();
            Map(x => x.PageId).Column("page_id").Not.Nullable();
        }
    }
}
