using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings;
using RoomInfoUpdatedComposer = AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings.RoomInfoUpdatedComposer;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    public class SaveRoomSettingsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            int roomId = msgEvent.ReadVL64();
            Room room = Engine.Locator.RoomController.GetRoom(roomId);

            if (room == null) return;

            string name = msgEvent.ReadString();
            string description = msgEvent.ReadString();
            int state = msgEvent.ReadVL64();
            string password = msgEvent.ReadString();
            int maxUsers = msgEvent.ReadVL64();
            int unk1 = msgEvent.ReadVL64();
            int unk2 = msgEvent.ReadVL64();
            int unk3 = msgEvent.ReadVL64();
            int categoryId = msgEvent.ReadVL64();
            int tagCount = msgEvent.ReadVL64();

            if (name.Length < 3) return;
            if (state < 0 || state > 2) return;
            if (state == 2 && password.Length < 3) return;
            if (maxUsers != 10 && maxUsers != 15 && maxUsers != 20 && maxUsers != 25) return;
            if (!Engine.Locator.NavigatorController.Categories.TryGetValue(categoryId, out RoomCategory category)) return;
            if (category.MinRank > client.Player.Rank) categoryId = 0;

            room.Name = name;
            room.Description = description;
            room.State = (RoomState) state;
            room.PlayersMax = maxUsers;
            room.CategoryId = categoryId;
            room.Save(("name", name), ("description", description), ("state", state), ("players_max", maxUsers), ("category_id", categoryId));

            Engine.Locator.RoomController.Rooms[roomId] = room;

            client.QueueComposer(new RoomSettingsSavedComposer(roomId));
            client.QueueComposer(new RoomInfoUpdatedComposer(roomId));
            client.QueueComposer(new GetGuestRoomResultComposer(room));
            client.Flush();
        }
    }
}