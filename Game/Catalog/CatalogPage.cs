namespace AuroraEmu.Game.Catalog
{
    public class CatalogPage
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int IconColor { get; set; }
        public virtual int IconImage { get; set; }
        public virtual bool Development { get; set; }
        public virtual bool Visible { get; set; }
        public virtual int ParentId { get; set; }
        public virtual int MinRank { get; set; }
    }
}
