using AuroraEmu.Game.Players;
using FluentNHibernate.Mapping;

namespace AuroraEmu.Database.Mappings
{
    public class PlayerMap : ClassMap<Player>
    {
        public PlayerMap()
        {
            Table("players");
            LazyLoad();
            Id(x => x.Id).Column("id").Length(11).GeneratedBy.Identity();
            Map(x => x.Username).Column("username").Length(15).Not.Nullable();
            Map(x => x.Password).Column("password").Length(80).Not.Nullable();
            Map(x => x.Email).Column("email").Length(30).Not.Nullable().Unique();
            Map(x => x.Gender).Column("gender").Length(1).Not.Nullable();
            Map(x => x.Figure).Column("figure").Length(80).Not.Nullable();
            Map(x => x.Motto).Column("motto").Length(40).Default("").Not.Nullable();
            Map(x => x.Coins).Column("coins").Length(11).Default("500").Not.Nullable();
            Map(x => x.Pixels).Column("pixels").Length(11).Default("0").Not.Nullable();
            Map(x => x.Rank).Column("rank").Length(2).Default("1").Not.Nullable();
            Map(x => x.HomeRoom).Column("home_room").Length(11).Default("0").Not.Nullable();
            Map(x => x.SSO).Column("sso_ticket").Length(40).Not.Nullable().Default("");
        }
    }
}
