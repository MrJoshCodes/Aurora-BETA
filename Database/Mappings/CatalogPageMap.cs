using AuroraEmu.Game.Catalog;
using FluentNHibernate.Mapping;

namespace AuroraEmu.Database.Mappings
{
    public class CatalogPageMap : ClassMap<CatalogPage>
    {
        public CatalogPageMap()
        {
            Table("catalog_pages");
            LazyLoad();
            Id(x => x.Id).Column("id").Length(11).GeneratedBy.Identity();
            Map(x => x.Name).Column("name").Length(20).Not.Nullable();
            Map(x => x.IconColor).Column("icon_color").Length(2).Not.Nullable();
            Map(x => x.IconImage).Column("icon_image").Not.Nullable().Length(2);
            Map(x => x.Development).Column("in_development").Not.Nullable();
            Map(x => x.Visible).Column("is_visible").Not.Nullable();
            Map(x => x.ParentId).Column("parent_id").Length(11).Not.Nullable();
            Map(x => x.MinRank).Column("min_rank").Length(2).Not.Nullable();
            Map(x => x.Layout).Column("layout").Length(15).Not.Nullable();
            Map(x => x.HasContent).Column("has_content").Not.Nullable();
        }
    }
}
