using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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

        public Dictionary<int, int> GetUserAchievements(int playerId)
        {
            var userAchievements = new Dictionary<int, int>();

            using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM `player_achievements` WHERE `player_id` = @playerId");
                dbConnection.AddParameter("@playerId", playerId);

                using (var reader = dbConnection.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userAchievements.Add(reader.GetInt32("achievement_id"), reader.GetInt32("level"));
                    }
                }
            }

            return userAchievements;
        }

        public Dictionary<int, int> GetUserAchievementProgresses(int playerId)
        {
            var userAchievementProgresses = new Dictionary<int, int>();

            using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM player_achievement_progress WHERE player_id = @playerId");
                dbConnection.AddParameter("@playerId", playerId);

                using (var reader = dbConnection.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        userAchievementProgresses.Add(reader.GetInt32("achievement_id"), reader.GetInt32("progress"));
                    }
                }
            }

            return userAchievementProgresses;
        }

        public void AddOrUpdateUserAchievementProgress(int playerId, int achievementId, int progress)
        {
            using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO player_achievement_progress VALUES (@playerId, @achievementId, @progress) ON DUPLICATE KEY UPDATE progress = VALUES(progress)");
                dbConnection.AddParameter("@playerId", playerId);
                dbConnection.AddParameter("@achievementId", achievementId);
                dbConnection.AddParameter("@progress", progress);
                dbConnection.Execute();
            }
        }

        public void AddOrUpdateUserAchievement(int playerId, int achievementId, int level)
        {
            using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO `player_achievements` VALUES (@achievementId, @level, @playerId) ON DUPLICATE KEY UPDATE `level` = VALUES(`level`)");
                dbConnection.AddParameter("@playerId", playerId);
                dbConnection.AddParameter("@achievementId", achievementId);
                dbConnection.AddParameter("@level", level);
                dbConnection.Execute();
            }
        }
    }
}