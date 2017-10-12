using AuroraEmu.DI.Database.DAO;
using AuroraEmu.DI.Game.Subscription;
using AuroraEmu.Game.Subscription.Models;
using System.Collections.Generic;

namespace AuroraEmu.Game.Subscription
{
    public class SubscriptionController : ISubscriptionController
    {
        public ISubscriptionDao Dao { get; }

        public SubscriptionController(ISubscriptionDao dao)
        {
            Dao = dao;
        }

        public void GetSubscriptionData(Dictionary<string, SubscriptionData> data, int userId) =>
            Dao.GetSubscriptionData(data, userId);
    }
}