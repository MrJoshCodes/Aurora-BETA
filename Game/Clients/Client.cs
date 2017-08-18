using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game.Packets;
using DotNetty.Transport.Channels;
using System.Collections.Generic;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Game.Players.Components;

namespace AuroraEmu.Game.Clients
{
    public class Client
    {
        private readonly IChannel _channel;

        public Player Player { get; private set; }

        public int? RoomCount { get; set; }
        public Room LoadingRoom { get; set; }
        public Room CurrentRoom { get; set; }
        public UserActor UserActor { get; set; }

        public Dictionary<int, Item> Items { get; set; }

        public Client(IChannel channel)
        {
            _channel = channel;
        }

        public void Disconnect()
        {
            _channel.DisconnectAsync();
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
            Engine.Logger.Info($"{System.Text.Encoding.Default.GetString(composer.GetBytes().Array).Replace("\r", "{13}")}");
            if (flush)
            {
                _channel.WriteAndFlushAsync(composer.GetBytes());
            } else
            {
                _channel.WriteAsync(composer.GetBytes());
            }
        }

        public IChannel Flush()
        {
            return _channel.Flush();
        }

        public void Login(string sso)
        {
            Player = Engine.MainDI.PlayerController.GetPlayerBySSO(sso);

            if (Player != null)
            {
                SendComposer(new MessageComposer(3));

                MessageComposer composer = new MessageComposer(139);
                composer.AppendString($"Welcome {Player.Username} to Aurora BETA, enjoy your stay!");
                SendComposer(composer);

                Player.BadgesComponent = new BadgesComponent(Player.Id);
            }
            else
            {
                Disconnect();
            }
        }
    }
}
