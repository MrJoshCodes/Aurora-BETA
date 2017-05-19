using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets
{
    public interface IPacketEvent
    {
        void Run(Client client, MessageEvent msgEvent);
    }
}
