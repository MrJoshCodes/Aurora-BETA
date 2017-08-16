using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Players;
using DotNetty.Transport.Channels;

namespace AuroraEmu.DI.Game.Clients
{
    public interface IClientController
    {
        void AddClient(IChannel channel);

        Client GetClient(IChannel channel);

        void RemoveClient(IChannel channel);

        bool TryGetPlayer(int playerId, out Player player);

        Client GetClientByHabbo(int habboId);
    }
}
