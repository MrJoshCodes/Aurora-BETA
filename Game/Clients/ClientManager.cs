using DotNetty.Transport.Channels;
using System.Collections.Generic;

namespace AuroraEmu.Game.Clients
{
    public class ClientManager
    {
        public readonly Dictionary<IChannelId, Client> clients;

        public ClientManager()
        {
            clients = new Dictionary<IChannelId, Client>();
        }

        public void AddClient(IChannel channel)
        {
            Client client;

            if (clients.TryGetValue(channel.Id, out client))
            {
                client.Disconnect();
            }
            else
            {
                client = new Client(channel);

                clients.Add(channel.Id, client);
            }
        }

        public Client GetClient(IChannel channel)
        {
            Client client;

            if (clients.TryGetValue(channel.Id, out client))
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
            clients.Remove(channel.Id);
        }
    }
}
