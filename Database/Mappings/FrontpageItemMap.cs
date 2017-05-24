using AuroraEmu.Game.Navigator;
using FluentNHibernate.Mapping;

namespace AuroraEmu.Database.Mappings
{
    class FrontpageItemMap : ClassMap<FrontpageItem>
    {
        public FrontpageItemMap()
        {
            Table("frontpage_items");
            LazyLoad();
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Name).Column("name").Length(20).Not.Nullable();
            Map(x => x.Description).Column("description").CustomSqlType("text").Not.Nullable();
            Map(x => x.Image).Column("image").CustomSqlType("text").Not.Nullable();
            Map(x => x.Size).Column("size").Not.Nullable();
            Map(x => x.Type).Column("type").Not.Nullable();
            Map(x => x.Tag).Column("tag").Length(20).Not.Nullable();
        }
    }
}
