using Newtonsoft.Json;
using System.IO;

namespace AuroraEmu.Config
{
    public class ConfigLoader
    {
        private static ConfigLoader instance;

        public DatabaseConfig DbConfig { get; private set; }
        public HabboHotelConfig HHConfig { get; private set; }

        public ConfigLoader()
        {
            DbConfig = JsonConvert.DeserializeObject<DatabaseConfig>(GetJSON("db.settings.json"));
            HHConfig = JsonConvert.DeserializeObject<HabboHotelConfig>(GetJSON("habbohotel.settings.json"));
        }

        /// <summary>
        /// Gets the JSON from a file.
        /// </summary>
        /// <param name="file">The file name to read</param>
        /// <returns></returns>
        public string GetJSON(string file)
        {
            using (StreamReader reader = new StreamReader(File.Open(file, FileMode.Open)))
            {
                return reader.ReadToEnd();
            }
        }

        public static ConfigLoader GetInstance()
        {
            if (instance == null)
                instance = new ConfigLoader();

            return instance;
        }
    }
}
