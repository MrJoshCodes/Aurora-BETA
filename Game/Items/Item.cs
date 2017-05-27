using System.Data;

namespace AuroraEmu.Game.Items
{
    public class Item
    {
        public int Id { get; set; }
        public string SwfName { get; set; }
        public char SpriteType { get; set; }
        public int SpriteId { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public double Height { get; set; }
        public bool CanStack { get; set; }
        public string ItemType { get; set; }
        public bool CanGift { get; set; }
        public bool CanRecycle { get; set; }
        public bool InteractorRightsRequired { get; set; }
        public string InteractorType { get; set; }
        public string VendorIDs { get; set; }

        public Item(DataRow row)
        {
            this.Id = (int)row["Id"];
            this.SwfName = (string)row["swf_name"];
            this.SpriteType = (char)row["sprite_type"];
            this.SpriteId = (int)row["sprite_id"];
            this.Length = (int)row["length"];
            this.Width = (int)row["width"];
            this.Height = (double)row["height"];
            this.CanStack = (bool)row["can_stack"];
            this.ItemType = (string)row["item_type"];
            this.CanGift = (bool)row["can_gift"];
            this.CanRecycle = (bool)row["can_recycle"];
            this.InteractorRightsRequired = (bool)row["interactor_requires_rights"];
            this.InteractorType = (string)row["interactor_type"];
            this.VendorIDs = (string)row["vendor_ids"];
        }
    }
}
