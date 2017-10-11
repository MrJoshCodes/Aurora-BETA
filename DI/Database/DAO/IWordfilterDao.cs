using AuroraEmu.Game.Wordfilter.Models;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IWordfilterDao
    {
        void WordfilterData(List<Wordfilter> wordFilter);
    }
}