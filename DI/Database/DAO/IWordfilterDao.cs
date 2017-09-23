using AuroraEmu.Game.Wordfilter;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IWordfilterDao
    {
        void WordfilterData(List<Wordfilter> wordFilter);
    }
}