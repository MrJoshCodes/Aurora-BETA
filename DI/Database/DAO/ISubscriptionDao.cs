using AuroraEmu.Game.Subscription.Models;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface ISubscriptionDao
    {
        void GetSubscriptionData(Dictionary<string, SubscriptionData> data, int userId);
    }
}
