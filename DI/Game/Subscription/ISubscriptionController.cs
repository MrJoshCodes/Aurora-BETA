using AuroraEmu.Game.Subscription;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Subscription
{
    public interface ISubscriptionController
    {
        Dictionary<string, SubscriptionData> GetSubscriptionData(Dictionary<string, SubscriptionData> data, int userId);
    }
}
