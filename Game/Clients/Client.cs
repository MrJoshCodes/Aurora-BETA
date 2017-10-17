using AuroraEmu.Network.Game.Packets;
using DotNetty.Transport.Channels;
using System.Collections.Generic;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Game.Players.Components;
using DotNetty.Buffers;
using System;
using AuroraEmu.Network.Game.Packets.Composers.Users;
using AuroraEmu.Network.Game.Packets.Composers.Misc;
using AuroraEmu.Network.Game.Packets.Composers.Moderation;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Players.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Game.Subscription.Models;

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

        public void Disconnect() {

            if (this.CurrentRoom != null && this.UserActor != null)
                this.CurrentRoom.RemoveActor(UserActor, true);

            _channel.DisconnectAsync();
        }

        public void SendComposer(MessageComposer composer) =>
            Send(composer, true);

        public void QueueComposer(MessageComposer composer) =>
            Send(composer, false);

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

        public IChannel Flush() =>
            _channel.Flush();

        public void Login(string sso)
        {
            Player = Engine.Locator.PlayerController.GetPlayerBySSO(sso);

            if (Player != null)
            {
                QueueComposer(new UserRightsMessageComposer());
                QueueComposer(new MessageComposer(3));
                QueueComposer(new HabboBroadcastMessageComposer($"Welcome {Player.Username} to Aurora BETA, enjoy your stay!"));

                if (Player.Rank > 5)
                {
                    QueueComposer(new ModeratorInitMessageComposer());
                }

                Flush();
                
                Player.BadgesComponent = new BadgesComponent(Player.Id);
                Player.MessengerComponent = new MessengerComponent(Player);
                Engine.Locator.SubscriptionController.GetSubscriptionData(SubscriptionData, Player.Id);
            }
            else
            {
                Disconnect();
            }
        }


        public void IncreaseCredits(int amount)
        {
            Player.Coins += amount;
            SendComposer(new CreditBalanceMessageComposer(Player.Coins));
        }

        public void DecreaseCredits(int amount)
        {
            Player.Coins -= amount;
            SendComposer(new CreditBalanceMessageComposer(Player.Coins));
        }

        public void IncreasePixels(int amount)
        {
            Player.Pixels += amount;
            SendComposer(new HabboActivityPointNotificationMessageComposer(Player.Pixels, 0));
        }


        public void DecreasePixels(int amount)
        {
            Player.Pixels -= amount;
            SendComposer(new HabboActivityPointNotificationMessageComposer(Player.Pixels, 0));
        }

        public void Dispose()
        {
            if (Player != null)
            {
                using (Player player = Player)
                {
                    SubscriptionData.Clear();
                    Items.Clear();
                    using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
                    {
                        dbConnection.SetQuery("UPDATE players SET coins = @coins, pixels = @pixels WHERE id = @id");
                        dbConnection.AddParameter("@coins", player.Coins);
                        dbConnection.AddParameter("@pixels", player.Pixels);
                        dbConnection.AddParameter("@id", player.Id);
                        dbConnection.Execute();
                    }
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
