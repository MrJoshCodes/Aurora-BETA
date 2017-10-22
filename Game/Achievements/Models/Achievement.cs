using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Achievements.Models
{
    public class Achievement
    {
        public int Id { get; }
        public string Badge { get; }
        
        public Dictionary<int, AchievementLevel> Levels { get; }

        public Achievement(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Badge = reader.GetString("badge");
            Levels = new Dictionary<int, AchievementLevel>();
        }
    }
}