using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game.Packets;
using DotNetty.Transport.Channels;
using System.Collections.Generic;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Game.Players.Components;
using AuroraEmu.Database.DAO;
using DotNetty.Buffers;

namespace AuroraEmu.Game.Clients
{
    public class Client
    {
        private readonly IChannel _channel;

        public Player Player { get; private set; }

        public int? RoomCount { get; set; }
        public int LoadingRoomId { get; set; }
        public int CurrentRoomId { get; set; }
        public UserActor UserActor { get; set; }

        public Dictionary<int, Item> Items { get; set; }

        public Client(IChannel channel)
        {
            _channel = channel;
            Items = new Dictionary<int, Item>();
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
            Engine.Logger.Info(System.Text.Encoding.GetEncoding(0).GetString(composer.GetBytes()));
            if (flush)
            {
                _channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(composer.GetBytes()));
            } else
            {
                _channel.WriteAsync(Unpooled.CopiedBuffer(composer.GetBytes()));
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
                Player.MessengerComponent = new MessengerComponent(Player);
            }
            else
            {
                Disconnect();
            }
        }


        public void IncreaseCredits(int amount)
        {
            Player.Coins += amount;
            Engine.MainDI.PlayerDao.UpdateCurrency(Player.Id, Player.Coins, "coins");
            SendComposer(new Network.Game.Packets.Composers.Users.CreditBalanceMessageComposer(Player.Coins));
        }

        public void DecreaseCredits(int amount)
        {
            Player.Coins -= amount;
            Engine.MainDI.PlayerDao.UpdateCurrency(Player.Id, Player.Coins, "coins");
            SendComposer(new Network.Game.Packets.Composers.Users.CreditBalanceMessageComposer(Player.Coins));
        }

        public void IncreasePixels(int amount)
        {
            Player.Pixels += amount;
            Engine.MainDI.PlayerDao.UpdateCurrency(Player.Id, Player.Pixels, "pixels");
            SendComposer(new Network.Game.Packets.Composers.Users.HabboActivityPointNotificationMessageComposer(Player.Pixels, 0));
        }


        public void DecreasePixels(int amount)
        {
            Player.Pixels -= amount;
            Engine.MainDI.PlayerDao.UpdateCurrency(Player.Id, Player.Pixels, "pixels");
            SendComposer(new Network.Game.Packets.Composers.Users.HabboActivityPointNotificationMessageComposer(Player.Pixels, 0));
        }
    }
}
