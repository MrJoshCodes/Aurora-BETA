using System.Threading.Tasks;

namespace AuroraEmu.DI.Network.Game
{
    public interface IGameNetworkListener
    {
        Task RunServer();
    }
}
