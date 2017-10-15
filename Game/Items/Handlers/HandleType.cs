namespace AuroraEmu.Game.Items.Handlers
{
    public enum HandleType
    {
        NONE,
        DICE,
        COLOR_WHEEL,
        DIMMER,
        SWITCH
    }

    public static class HandlerParser
    {
        public static HandleType GetItemHandle(string interactionType)
        {
            switch(interactionType.ToLower())
            {
                case "dice":
                    return HandleType.DICE;
                case "color_wheel":
                    return HandleType.COLOR_WHEEL;
                case "dimmer":
                    return HandleType.DIMMER;
                case "switch":
                    return HandleType.SWITCH;
                default:
                    return HandleType.NONE;
            }
        }
    }
}
