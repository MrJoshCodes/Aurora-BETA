using AuroraEmu.Database;
using AuroraEmu.Game;
using AuroraEmu.Network.Game;
using log4net;

namespace AuroraEmu
{
    public class Engine
    {
        public static ILog Logger { get; private set; }

        static void Main(string[] args)
        {
            Logger = LogManager.GetLogger(typeof(Engine));

            GameNetworkListener.GetInstance();
            Aurora.GetInstance();

            System.Console.ReadLine();
        }
    }
}
