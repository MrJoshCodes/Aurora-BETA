using AuroraEmu.Game.Subscription;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Subscription
{
    public interface ISubscriptionController
    {
       void GetSubscriptionData(Dictionary<string, SubscriptionData> data, int userId);
    }
}
