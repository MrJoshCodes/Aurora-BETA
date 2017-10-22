using System.Collections.Generic;
using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.DI.Game.Achievements
{
    public interface IAchievementController
    {
        Dictionary<string, Achievement> Achievements { get; }
    }
}