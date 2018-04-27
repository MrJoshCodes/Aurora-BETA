using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Game.Players.Components;
using AuroraEmu.Network.Game.Packets.Composers.Users;
using AuroraEmu.Network.Game.Packets.Composers.Misc;
using AuroraEmu.Network.Game.Packets.Composers.Moderation;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Players.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Game.Subscription.Models;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

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
        public Dictionary<int, int> Achievements { get; set; }

        public Client(IChannel channel)
        {
            _channel = channel;
            Items = new Dictionary<int, Item>();
            SubscriptionData = new Dictionary<string, SubscriptionData>();
        }

        public void Disconnect()
        {

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

        public async void Close() =>
            await _channel.CloseAsync();

        public void Login(string sso)
        {
            Player = Engine.Locator.PlayerController.GetPlayerBySSO(sso);

            if (Player != null)
            {
                QueueComposer(new UserRightsMessageComposer());
                QueueComposer(new MessageComposer(3));
                QueueComposer(new ModMessageComposer($"Welcome {Player.Username} to Aurora BETA, enjoy your stay! Please report any bugs to us and we'll sort it ASAP.", "URL"));
                QueueComposer(new NavigatorSettingsComposer(Player.HomeRoom));
                
                if (Player.Rank > 5)
                {
                    QueueComposer(new ModeratorInitMessageComposer());
                }

                Flush();
                
                Player.BadgesComponent = new BadgesComponent(Player.Id);
                Player.MessengerComponent = new MessengerComponent(Player);
                Engine.Locator.SubscriptionController.GetSubscriptionData(SubscriptionData, Player.Id);
                Achievements = Engine.Locator.AchievementController.Dao.GetUserAchievements(Player.Id);

                if (Player.FavouriteGroupId != -1)
                    Player.Group = Engine.Locator.GroupController.Dao.GetGroup(Player.FavouriteGroupId);
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
            SendComposer(new HabboActivityPointNotificationMessageComposer(Player.Pixels, amount));
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
                        dbConnection.SetQuery("UPDATE players SET coins = @coins, pixels = @pixels, figure = @figure, gender = @gender WHERE id = @id");
                        dbConnection.AddParameter("@coins", player.Coins);
                        dbConnection.AddParameter("@pixels", player.Pixels);
                        dbConnection.AddParameter("@figure", player.Figure);
                        dbConnection.AddParameter("@gender", player.Gender);
                        dbConnection.AddParameter("@id", player.Id);
                        dbConnection.Execute();
                    }
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
