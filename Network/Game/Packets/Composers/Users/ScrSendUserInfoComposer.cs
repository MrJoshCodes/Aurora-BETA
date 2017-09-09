using AuroraEmu.Game.Subscription;
using System;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class ScrSendUserInfoComposer : MessageComposer
    {
        public ScrSendUserInfoComposer(SubscriptionData data)
            : base(7)
        {
            AppendString(data.Subscription.ToLower());
            double timeLeft = data.TimeExpire - Engine.GetUnixTimeStamp();
            int totalDaysLeft = (int)Math.Ceiling(timeLeft / 86400);
            int monthsLeft = totalDaysLeft / 31;

            if (monthsLeft >= 1) monthsLeft--;

            AppendVL64(totalDaysLeft - (monthsLeft * 31));
            AppendVL64(true);
            AppendVL64(monthsLeft);
        }
    }
}
