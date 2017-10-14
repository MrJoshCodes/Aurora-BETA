using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.DI.Game.Wordfilter
{
    public interface IWordfilterController
    {
        IWordfilterDao Dao { get; }

        void Init();

        string CheckString(string message);
    }
}
