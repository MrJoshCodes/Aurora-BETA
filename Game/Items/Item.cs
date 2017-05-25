namespace AuroraEmu.Game.Items
{
    public class Item
    {
        public virtual int Id { get; set; }
        public virtual string SwfName { get; set; }
        public virtual char SpriteType { get; set; }
        public virtual int SpriteId { get; set; }
        public virtual int Width { get; set; }
        public virtual int Length { get; set; }
        public virtual double Height { get; set; }
        public virtual bool CanStack { get; set; }
        public virtual string ItemType { get; set; }
        public virtual bool CanGift { get; set; }
        public virtual bool CanRecycle { get; set; }
        public virtual bool InteractorRightsRequired { get; set; }
        public virtual string InteractorType { get; set; }
        public virtual string VendorIDs { get; set; }
    }
}
