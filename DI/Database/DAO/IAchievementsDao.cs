using System.Collections.Generic;
using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IAchievementsDao
    {
        Dictionary<string, Achievement> GetAchievements();
    }
}