using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Achievements.Models;
using AuroraEmu.Game.Clients;

namespace AuroraEmu.DI.Game.Achievements
{
    public interface IAchievementController
    {
        IAchievementsDao Dao { get; }
        Dictionary<string, Achievement> Achievements { get; }

        void CheckAchievement(Client client, string achievement, int current);
        void UpdateAchievementProgress(Client client, string achievementCode);
    }
}