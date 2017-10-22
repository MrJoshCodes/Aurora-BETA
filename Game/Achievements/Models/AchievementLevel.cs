using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Achievements.Models
{
    public class AchievementLevel
    {
        public int Level { get; }
        public int Pixels { get; }
        public int Required { get; }

        public AchievementLevel(MySqlDataReader reader)
        {
            Level = reader.GetInt32("level");
            Pixels = reader.GetInt32("pixels");
            Required = reader.GetInt32("required");
        }
    }
}