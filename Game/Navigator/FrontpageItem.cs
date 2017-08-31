using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Navigator
{
    public class FrontpageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string Image { get; set; }
        public int Type { get; set; }
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
        }
    }
}