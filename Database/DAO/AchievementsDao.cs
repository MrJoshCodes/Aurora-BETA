using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.Database.DAO
{ 
    public class AchievementsDao : IAchievementsDao
    {
        public Dictionary<string, Achievement> GetAchievements()
        {
            var achievements = new Dictionary<string, Achievement>();

            using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM `achievements`");

                using (var reader = dbConnection.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var badge = reader.GetString("badge");

                        if (!achievements.ContainsKey(badge))
                        {
                            achievements.Add(badge, new Achievement(reader));
                        }
                        
                        achievements[badge].Levels.Add(reader.GetInt32("level"), new AchievementLevel(reader));
                    }
                }
            }

            return achievements;
        }
    }
}