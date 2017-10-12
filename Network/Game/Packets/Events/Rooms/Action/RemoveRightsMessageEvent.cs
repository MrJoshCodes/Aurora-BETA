using AuroraEmu.Database;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings;
using Newtonsoft.Json;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Action
{
    public class RemoveRightsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int amount = msgEvent.ReadVL64();

            for (int i = 0; i < amount; i++)
            {
                int userId = msgEvent.ReadVL64();
                if (!client.CurrentRoom.UserRights.Contains(userId)) return;
                Client player = Engine.Locator.ClientController.GetClientByHabbo(userId);
                if (player != null)
                {
                    if (player.UserActor.Statusses.ContainsKey("flatcrtl"))
                        player.UserActor.Statusses.Remove("flatcrtl");
                    client.CurrentRoom.UserRights.Remove(userId);
                    player.SendComposer(new YouAreNotControllerMessageComposer());
                    player.SendComposer(new FlatControllerRemovedMessageComposer(client.CurrentRoomId, userId));
                }
            }

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
