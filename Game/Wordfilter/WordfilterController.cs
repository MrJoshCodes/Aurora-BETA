using AuroraEmu.DI.Game.Wordfilter;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AuroraEmu.Game.Wordfilter
{
    public class WordfilterController : IWordfilterController
    {
        private readonly List<Wordfilter> _filteredWords;

        public WordfilterController()
        {
            _filteredWords = new List<Wordfilter>();

            Init();
        }

        public void Init()
        {
            Engine.MainDI.WordfilterDao.WordfilterData(_filteredWords);

            Engine.Logger.Info($"Loaded {_filteredWords.Count} filtered words.");
        }

        public string CheckString(string message)
        {
            foreach (Wordfilter filter in _filteredWords.ToList())
            {
                if (message.ToLower().Contains(filter.Word) || message == filter.Word)
                {
                    message = Regex.Replace(message, filter.Word, filter.ReplacementWord, RegexOptions.IgnoreCase);
                }
            }
            return message;
        }
    }
}