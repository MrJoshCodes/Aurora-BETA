
namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class SlideObjectBundleMessageEvent : MessageComposer
    {
        public SlideObjectBundleMessageEvent(int itemId, int oldX, int oldY, int newX, int newY, int rollerId, double oldHeight, double newHeight) : base(230)
		{
			AppendVL64(oldX);
			AppendVL64(oldY);

			AppendVL64(newX);
			AppendVL64(newY);

			AppendVL64(1);

			AppendVL64(itemId);

		    AppendString("" + oldHeight);
			AppendString("" + newHeight);

			AppendVL64(itemId);
		}

    }
}
