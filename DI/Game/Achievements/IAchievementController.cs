using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.DI.Game.Achievements
{
    public interface IAchievementController
    {
        IAchievementsDao Dao { get; }
        Dictionary<string, Achievement> Achievements { get; }
    }
}