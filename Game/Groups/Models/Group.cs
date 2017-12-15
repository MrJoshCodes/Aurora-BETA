using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Groups.Models
{
    public class Group
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Badge { get; private set; }

        public Group(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("name");
            Description = reader.GetString("description");
            Badge = reader.GetString("badge");
        }
    }
}