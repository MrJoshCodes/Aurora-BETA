namespace AuroraEmu.Game.Items.Handlers
{
    public enum HandleType
    {
        NONE,
        DICE
    }

    public static class HandlerParser
    {
        public static HandleType GetItemHandle(string interactionType)
        {
            switch(interactionType.ToLower())
            {
                case "dice":
                    return HandleType.DICE;
                default:
                    return HandleType.NONE;
            }
        }
    }
}
