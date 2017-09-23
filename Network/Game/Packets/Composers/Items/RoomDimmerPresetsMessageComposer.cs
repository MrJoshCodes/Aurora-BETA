using AuroraEmu.Game.Items.Dimmer;

namespace AuroraEmu.Network.Game.Packets.Composers.Items
{
    public class RoomDimmerPresetsMessageComposer : MessageComposer
    {
        public RoomDimmerPresetsMessageComposer(DimmerData data) : base(365)
        {
            AppendVL64(data.Presets.Count);
            AppendVL64(data.CurrentPreset);

            int i = 0;

            foreach(DimmerPreset preset in data.Presets)
            {
                i++;

                AppendVL64(i);
                AppendVL64((preset.BackgroundOnly ? 1 : 0) + 1);
                AppendString(preset.ColorCode);
                AppendVL64(preset.ColorIntensity);
            }
        }
    }
}
