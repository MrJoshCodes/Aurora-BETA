using AuroraEmu.DI.Game.Clients;
using AuroraEmu.Game.Players;
using DotNetty.Transport.Channels;
using System.Collections.Generic;

namespace AuroraEmu.Game.Clients
{
    public class ClientController : IClientController
    {
        public readonly Dictionary<IChannelId, Client> Clients;

        public ClientController()
        {
            Clients = new Dictionary<IChannelId, Client>();
        }

        public void AddClient(IChannel channel)
        {
            if (Clients.TryGetValue(channel.Id, out Client client))
            {
                client.Disconnect();
            }
            else
            {
                client = new Client(channel);

                Clients.Add(channel.Id, client);
            }
        }

        public Client GetClient(IChannel channel)
        {
            if (Clients.TryGetValue(channel.Id, out Client client))
            {
                return client;
            }
            else
            {
                channel.DisconnectAsync();

                return null;
            }
        }

        public void RemoveClient(IChannel channel)
        {
            Clients.Remove(channel.Id);
        }
        
        public bool TryGetPlayer(int playerId, out Player player)
        {
            foreach (Client client in Clients.Values)
            {
                if (client.Player.Id == playerId)
                {
                    player = client.Player;

                    return true;
                }
            }

            player = null;

            return false;
        }

        public Client GetClientByHabbo(int habboId)
        {
            foreach (Client client in Clients.Values)
            {
                if (client.Player.Id == habboId)
                    return client;
            }

            return null;
        }
    }
}
