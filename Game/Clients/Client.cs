using System;
using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game.Packets;
using DotNetty.Transport.Channels;

namespace AuroraEmu.Game.Clients
{
    public class Client
    {
        private IChannel channel;

        public Player Player { get; private set; }

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
            channel.WriteAndFlushAsync(composer.GetBytes());
        }

        public void Login(string sso)
        {
            Player = Engine.Game.Players.GetPlayerBySSO(sso);

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
