using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game.Packets;
using DotNetty.Transport.Channels;
using System.Collections.Generic;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Game.Players.Components;
using DotNetty.Buffers;
using AuroraEmu.Game.Subscription;
using System;

namespace AuroraEmu.Game.Clients
{
    public class Client : IDisposable
    {
        private readonly IChannel _channel;

        public Player Player { get; private set; }

        public int? RoomCount { get; set; }
        public int LoadingRoomId { get; set; }
        public int CurrentRoomId { get; set; }
        public UserActor UserActor { get; set; }
        public Room CurrentRoom { get; set; }

        public Dictionary<int, Item> Items { get; set; }
        public Dictionary<string, SubscriptionData> SubscriptionData { get; set; }

        public Client(IChannel channel)
        {
            _channel = channel;
            Items = new Dictionary<int, Item>();
            SubscriptionData = new Dictionary<string, SubscriptionData>();
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
                Engine.MainDI.SubscriptionController.GetSubscriptionData(SubscriptionData, Player.Id);
            }
            else
            {
                Disconnect();
            }
        }


        public void IncreaseCredits(int amount)
        {
            Player.Coins += amount;
            SendComposer(new Network.Game.Packets.Composers.Users.CreditBalanceMessageComposer(Player.Coins));
        }

        public void DecreaseCredits(int amount)
        {
            Player.Coins -= amount;
            SendComposer(new Network.Game.Packets.Composers.Users.CreditBalanceMessageComposer(Player.Coins));
        }

        public void IncreasePixels(int amount)
        {
            Player.Pixels += amount;
            SendComposer(new Network.Game.Packets.Composers.Users.HabboActivityPointNotificationMessageComposer(Player.Pixels, 0));
        }


        public void DecreasePixels(int amount)
        {
            Player.Pixels -= amount;
            SendComposer(new Network.Game.Packets.Composers.Users.HabboActivityPointNotificationMessageComposer(Player.Pixels, 0));
        }

        public void Dispose()
        {
            Player.BadgesComponent.Badges.Clear();
            Player.MessengerComponent.Friends.Clear();
            Player.MessengerComponent.Requests.Clear();
            using (var dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery("UPDATE player SET coins = @coins, pixels = @pixels WHERE username = @username");
                dbClient.AddParameter("@coins", Player.Coins);
                dbClient.AddParameter("@pixels", Player.Pixels);
                dbClient.AddParameter("@username", Player.Username);
                dbClient.Execute();
            }
            GC.SuppressFinalize(this);
        }
    }
}
