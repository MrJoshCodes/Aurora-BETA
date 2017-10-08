using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Action
{
    public class AssignRightsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int userId = msgEvent.ReadVL64();
            System.Console.WriteLine(userId);

            Client player = Engine.MainDI.ClientController.GetClientByHabbo(userId);
            if (player != null)
            {
                client.SendComposer(new FlatControllerAddedComposer(player.UserActor));
                player.UserActor.Statusses.Add("flatcrtl", "");
                player.UserActor.UpdateNeeded = true;

                player.SendComposer(new YouAreControllerMessageComposer());
            }
        }
    }
}
