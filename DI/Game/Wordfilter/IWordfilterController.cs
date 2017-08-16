namespace AuroraEmu.DI.Game.Wordfilter
{
    public interface IWordfilterController
    {
        void Init();

        string CheckString(string message);
    }
}
