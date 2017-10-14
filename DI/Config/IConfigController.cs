using AuroraEmu.Config;

namespace AuroraEmu.DI.Config
{
    public interface IConfigController
    {
        DatabaseConfig DbConfig { get; set; }

        HabboHotelConfig HHConfig { get; set; }

        string GetJSON(string file);
    }
}
