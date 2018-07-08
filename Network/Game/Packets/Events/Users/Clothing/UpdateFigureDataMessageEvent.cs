using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Users;

namespace AuroraEmu.Network.Game.Packets.Events.Users.Clothing
{
    public class UpdateFigureDataMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            string gender = msgEvent.ReadString();
            string figure = msgEvent.ReadString();

            client.Player.Figure = figure;
            client.Player.Gender = gender.ToLower();
            Engine.Locator.PlayerController.Dao.UpdateAppearance(client.Player.Id, figure, gender, client.Player.Motto);

            client.SendComposer(new UserChangeMessageComposer(client.Player));
            client.CurrentRoom.SendComposer(new UserChangeMessageComposer(client.UserActor.VirtualId, client.Player));
            
            Engine.Locator.AchievementController.CheckAchievement(client, "ACH_AvatarLooks", 1);
        }
    }
}
