using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Navigator.Models
{
    public class FrontpageItem
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public int Type { get; set; }
        public int RoomId { get; set; }
        public string ExternalText { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }

        public FrontpageItem(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("name");
            Description = reader.GetString("description");
            Size = reader.GetInt32("size");
            Image = reader.GetString("image");
            Type = reader.GetInt32("type");
            Tag = reader.GetString("tag");
            RoomId = reader.GetInt32("room_id");
            ExternalText = reader.GetString("external_text");
        }
    }
}