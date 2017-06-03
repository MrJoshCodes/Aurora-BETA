using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game.Packets;
using DotNetty.Transport.Channels;
using System.Collections.Generic;

namespace AuroraEmu.Game.Clients
{
    public class Client
    {
        private IChannel channel;

        public Player Player { get; private set; }
        public Dictionary<int, MessengerFriends> Friends { get; set; }

        public int? RoomCount { get; set; }

        public Client(IChannel channel)
        {
            this.channel = channel;
        }

        public void Disconnect()
        {
            channel.DisconnectAsync();
        }

        public void SendComposer(MessageComposer composer)
        {
            Send(composer, true);
        }

        public void QueueComposer(MessageComposer composer)
        {
            Send(composer, false);
        }

        public void Send(MessageComposer composer, bool flush)
        {
            if (flush)
            {
                channel.WriteAndFlushAsync(composer.GetBytes());
            } else
            {
                channel.WriteAsync(composer.GetBytes());
            }
        }

        public IChannel Flush()
        {
            return channel.Flush();
        }

        public void Login(string sso)
        {
            Player = PlayerController.GetInstance().GetPlayerBySSO(sso);

            if (Player != null)
            {
                SendComposer(new MessageComposer(3));

                MessageComposer composer = new MessageComposer(139);
                composer.AppendString($"Welcome {Player.Username} to Aurora BETA, enjoy your stay!");
                SendComposer(composer);
            }
            else
            {
                Disconnect();
            }
        }
    }
}
