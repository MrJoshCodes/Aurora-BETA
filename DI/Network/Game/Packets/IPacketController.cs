using AuroraEmu.Game.Clients;
using DotNetty.Buffers;

namespace AuroraEmu.DI.Network.Game.Packets
{
    public interface IPacketController
    {
        void LoadPackets();

        void Handle(Client client, IByteBuffer buffer);
    }
}
