namespace AuroraEmu.Game.Items.Handlers
{
    public enum HandleType
    {
        NONE,
        DICE,
        COLOR_WHEEL,
        DIMMER
    }

    public static class HandlerParser
    {
        public static HandleType GetItemHandle(string interactionType)
        {
            switch(interactionType.ToLower())
            {
                case "dice":
                    return HandleType.DICE;
                case "habbowheel":
                    return HandleType.COLOR_WHEEL;
                case "dimmer":
                    return HandleType.DIMMER;
                default:
                    return HandleType.NONE;
            }
        }
    }
}
