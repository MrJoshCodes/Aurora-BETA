using System.Data;

namespace AuroraEmu.Game.Items
{
    public class Item
    {
        public int Id { get; set; }
        public string SwfName { get; set; }
        public string SpriteType { get; set; }
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
            Id = (int)row["Id"];
            SwfName = (string)row["swf_name"];
            SpriteType = (string)row["sprite_type"];
            SpriteId = (int)row["sprite_id"];
            Length = (int)row["length"];
            Width = (int)row["width"];
            Height = (double)row["height"];
            CanStack = (bool)row["can_stack"];
            ItemType = (string)row["item_type"];
            CanGift = (bool)row["can_gift"];
            CanRecycle = (bool)row["can_recycle"];
            InteractorRightsRequired = (bool)row["interactor_requires_rights"];
            InteractorType = (string)row["interactor_type"];
            VendorIDs = (string)row["vendor_ids"];
        }
    }
}
