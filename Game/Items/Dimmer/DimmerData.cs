using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;

namespace AuroraEmu.Game.Items.Dimmer
{
    public class DimmerData
    {
        public int ItemId { get; set; }
        public int CurrentPreset { get; set; }
        public bool Enabled { get; set; }
        public List<DimmerPreset> Presets { get; set; }

        public DimmerData(MySqlDataReader reader, bool newDimmer)
        {
            Presets = new List<DimmerPreset>();
            ItemId = reader.GetInt32("item_id");
            Enabled = reader.GetBoolean("enabled");
            CurrentPreset = reader.GetInt32("current_preset");
            if (!newDimmer)
            {
                Presets.Add(new DimmerPreset(reader.GetString("preset_one")));
                Presets.Add(new DimmerPreset(reader.GetString("preset_two")));
                Presets.Add(new DimmerPreset(reader.GetString("preset_three")));
            }
            else
            {
                Presets.Add(new DimmerPreset("#000000,255,0"));
                Presets.Add(new DimmerPreset("#000000,255,0"));
                Presets.Add(new DimmerPreset("#000000,255,0"));
            }
        }

        public string GenerateExtradata()
        {
            DimmerPreset preset = Presets[CurrentPreset];
            StringBuilder stringBuilder = new StringBuilder();

            if (Enabled)
            {
                stringBuilder.Append(2);
            }
            else
            {
                stringBuilder.Append(1);
            }

            stringBuilder.Append(",");
            stringBuilder.Append(CurrentPreset);
            stringBuilder.Append(",");

            if (preset.BackgroundOnly)
            {
                stringBuilder.Append(2);
            }
            else
            {
                stringBuilder.Append(1);
            }

            stringBuilder.Append(",");
            stringBuilder.Append(preset.ColorCode);
            stringBuilder.Append(",");
            stringBuilder.Append(preset.ColorIntensity);
            return stringBuilder.ToString();
        }
    }
}
