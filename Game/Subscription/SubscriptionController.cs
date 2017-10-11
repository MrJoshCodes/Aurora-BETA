using AuroraEmu.DI.Game.Subscription;
using AuroraEmu.Game.Subscription.Models;
using System.Collections.Generic;

namespace AuroraEmu.Game.Subscription
{
    public class SubscriptionController : ISubscriptionController
    {
        public void GetSubscriptionData(Dictionary<string, SubscriptionData> data, int userId) =>
            Engine.MainDI.SubscriptionDao.GetSubscriptionData(data, userId);
    }
}