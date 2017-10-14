using AuroraEmu.Database;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings;
using Newtonsoft.Json;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Action
{
    public class AssignRightsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int userId = msgEvent.ReadVL64();

            if (client.CurrentRoom.UserRights.Contains(userId)) return;
            Client player = Engine.Locator.ClientController.GetClientByHabbo(userId);
            if (player != null)
            {
                client.SendComposer(new FlatControllerAddedComposer(player.UserActor));
                if (!player.UserActor.Statusses.ContainsKey("flatcrtl"))
                    player.UserActor.Statusses.Add("flatcrtl", "");
                player.UserActor.UpdateNeeded = true;
                client.CurrentRoom.UserRights.Add(userId);
                player.SendComposer(new YouAreControllerMessageComposer());

                string json = JsonConvert.SerializeObject(client.CurrentRoom.UserRights);
                using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
                {
                    dbConnection.SetQuery("UPDATE rooms SET user_rights = @data WHERE id = @roomId LIMIT 1");
                    dbConnection.AddParameter("@data", json);
                    dbConnection.AddParameter("@roomId", client.CurrentRoomId);
                    dbConnection.Execute();
                }
            }
        }
    }
}