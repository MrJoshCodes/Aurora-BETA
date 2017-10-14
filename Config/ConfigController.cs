using AuroraEmu.DI.Config;
using Newtonsoft.Json;
using System.IO;

namespace AuroraEmu.Config
{
    public class ConfigController : IConfigController
    {
        public DatabaseConfig DbConfig { get; set; }
        public HabboHotelConfig HHConfig { get; set; }

        public ConfigController()
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
    }
}
