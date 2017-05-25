using AuroraEmu.Game.Items;
using FluentNHibernate.Mapping;

namespace AuroraEmu.Database.Mappings
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Table("items");
            Not.LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.SwfName).Column("swf_name").Length(30).Not.Nullable();
            Map(x => x.SpriteType).Column("sprite_type").Length(1).Not.Nullable();
            Map(x => x.SpriteId).Column("sprite_id").Not.Nullable();
            Map(x => x.Length).Column("length").Not.Nullable();
            Map(x => x.Width).Column("width").Not.Nullable();
            Map(x => x.Height).Column("height").Not.Nullable();
            Map(x => x.CanStack).Column("can_stack").Not.Nullable();
            Map(x => x.ItemType).Column("item_type").Length(20).Not.Nullable();
            Map(x => x.CanGift).Column("can_gift").Not.Nullable();
            Map(x => x.CanRecycle).Column("can_recycle").Not.Nullable();
            Map(x => x.InteractorRightsRequired).Column("interactor_requires_rights");
            Map(x => x.InteractorType).Column("interactor_type").Length(20).Not.Nullable();
            Map(x => x.VendorIDs).Column("vendor_ids").CustomSqlType("text").Not.Nullable();
        }
    }
}
