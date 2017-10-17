using AuroraEmu.DI.Game.Clients;
using AuroraEmu.Game.Players.Models;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuroraEmu.Game.Clients
{
    public class ClientController : IClientController, IDisposable
    {
        public Dictionary<IChannelId, Client> Clients { get; }

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

        public void RemoveClient(IChannel channel) =>
            Clients.Remove(channel.Id);

        public Client GetClientByHabbo(int habboId) =>
            Clients.Values.Where(x => x.Player.Id == habboId).SingleOrDefault();

        public Client GetClientByHabbo(string habboName) =>
            Clients.Values.Where(x => x.Player.Username == habboName).SingleOrDefault();

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

        public void Dispose()
        {
            Parallel.ForEach(Clients.Values, client =>
            {
                client.Close();
            });

            Clients.Clear();
        }
    }
}