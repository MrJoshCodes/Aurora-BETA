using AuroraEmu.Game.Catalog;
using FluentNHibernate.Mapping;

namespace AuroraEmu.Database.Mappings
{
    public class CatalogPageDataMap : ClassMap<CatalogPageData>
    {
        public CatalogPageDataMap()
        {
            Table("catalog_pages_data");
            Id(x => x.Id).Column("id").Length(11).GeneratedBy.Identity();
            Map(x => x.Type).Column("type").Length(5).Not.Nullable();
            Map(x => x.Value).Column("value").Length(255).Not.Nullable().CustomSqlType("text");
            Map(x => x.PageId).Column("page_id").Not.Nullable();
        }
    }
}
