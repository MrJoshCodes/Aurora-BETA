using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Players.Models;
using DotNetty.Transport.Channels;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Clients
{
    public interface IClientController
    {
        Dictionary<IChannelId, Client> Clients { get; }

        void AddClient(IChannel channel);

        Client GetClient(IChannel channel);

        void RemoveClient(IChannel channel);

        bool TryGetPlayer(int playerId, out Player player);

        Client GetClientByHabbo(int habboId);

        Client GetClientByHabbo(string habboName);
    }
}
