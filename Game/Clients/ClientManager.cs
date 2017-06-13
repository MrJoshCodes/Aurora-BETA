using AuroraEmu.Game.Players;
using DotNetty.Transport.Channels;
using System.Collections.Generic;

namespace AuroraEmu.Game.Clients
{
    public class ClientManager
    {
        public readonly Dictionary<IChannelId, Client> clients;
        private static ClientManager clientManagerInstance;

        public ClientManager()
        {
            clients = new Dictionary<IChannelId, Client>();
        }

        public void AddClient(IChannel channel)
        {
            if (clients.TryGetValue(channel.Id, out Client client))
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
            if (clients.TryGetValue(channel.Id, out Client client))
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

        public bool PlayerIsOnline(int playerId)
        {
            return TryGetPlayer(playerId, out Player player);
        }

        public bool TryGetPlayer(int playerId, out Player player)
        {
            foreach (Client client in clients.Values)
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

        public Client GetClientByHabbo(int HabboId)
        {
            lock (this.clients)
            {
                Dictionary<IChannelId, Client>.Enumerator eClients = this.clients.GetEnumerator();

                while (eClients.MoveNext())
                {
                    Client Client = eClients.Current.Value;

                    if (Client.Player == null)
                    {
                        continue;
                    }

                    if (Client.Player.Id == HabboId)
                    {
                        return Client;
                    }
                }
            }

            return null;
        }

        public static ClientManager GetInstance()
        {
            if (clientManagerInstance == null)
                clientManagerInstance = new ClientManager();
            return clientManagerInstance;
        }
    }
}
