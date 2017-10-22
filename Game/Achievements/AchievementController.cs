using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.DI.Game.Achievements;
using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.Game.Achievements
{
    public class AchievementController : IAchievementController
    {
        public IAchievementsDao Dao { get; }
        public Dictionary<string, Achievement> Achievements { get; }

        public AchievementController(IAchievementsDao dao)
        {
            Dao = dao;
            Achievements = dao.GetAchievements();
            
            Engine.Logger.Info($"Loaded {Achievements.Count} Achievements.");
        }
    }
}