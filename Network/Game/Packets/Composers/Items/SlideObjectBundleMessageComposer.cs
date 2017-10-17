
namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class SlideObjectBundleMessageEvent : MessageComposer
    {
        public SlideObjectBundleMessageEvent(int actorId, int itemId, int oldX, int oldY, int newX, int newY, int rollerId, double oldHeight, double newHeight) : base(230)
		{
			AppendVL64(oldX);
			AppendVL64(oldY);

			AppendVL64(newX);
			AppendVL64(newY);

            if (itemId > 0) 
            {
                AppendVL64(1);
                AppendVL64(itemId);
            }
            else
            {
                AppendVL64(0);
                AppendVL64(rollerId);
                AppendVL64(2);
                AppendVL64(actorId);
            }

		    AppendString("" + oldHeight);
			AppendString("" + newHeight);
            
            if (itemId > 0) 
            {
			    AppendVL64(rollerId);
            }
		}

    }
}
