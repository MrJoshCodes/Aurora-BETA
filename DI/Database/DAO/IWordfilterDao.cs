using AuroraEmu.Game.Wordfilter;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IWordfilterDao
    {
        List<Wordfilter> WordfilterData(List<Wordfilter> wordFilter);
    }
}