using AuroraEmu.Game.Items.Handlers;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Items.Models
{
    public class ItemDefinition
    {
        public int Id { get; set; }
        public int SpriteId { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public bool CanStack { get; set; }
        public bool CanGift { get; set; }
        public bool CanRecycle { get; set; }
        public bool InteractorRightsRequired { get; set; }
        public string ItemType { get; set; }
        public string SwfName { get; set; }
        public string SpriteType { get; set; }
        public string InteractorType { get; set; }
        public string VendorIDs { get; set; }
        public double Height { get; set; }
        public HandleType HandleType { get; set; }

        public ItemDefinition(MySqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            SwfName = reader.GetString("swf_name");
            SpriteType = reader.GetString("sprite_type");
            SpriteId = reader.GetInt32("sprite_id");
            Length = reader.GetInt32("length");
            Width = reader.GetInt32("width");
            Height = reader.GetDouble("height");
            CanStack = reader.GetBoolean("can_stack");
            ItemType = reader.GetString("item_type");
            CanGift = reader.GetBoolean("can_gift");
            CanRecycle = reader.GetBoolean("can_recycle");
            InteractorRightsRequired = reader.GetBoolean("interactor_requires_rights");
            InteractorType = reader.GetString("interactor_type");
            HandleType = HandlerParser.GetItemHandle(InteractorType);
            VendorIDs = reader.GetString("vendor_ids");
        }
    }
}
