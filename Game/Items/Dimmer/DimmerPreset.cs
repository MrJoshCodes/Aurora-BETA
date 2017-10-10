namespace AuroraEmu.Game.Items.Dimmer
{
    public class DimmerPreset
    {
        public string ColorCode { get; set; }
        public int ColorIntensity { get; set; }
        public bool BackgroundOnly { get; set; }

        public DimmerPreset(string data)
        {
            string[] dataBits = data.Split(',');

            if (!IsValidColor(dataBits[0]))
            {
                dataBits[0] = "#000000";
            }

            ColorCode = dataBits[0];
            ColorIntensity = int.Parse(dataBits[1]);
            BackgroundOnly = dataBits[2] == "1" ? true : false;
        }

        public bool IsValidColor(string ColorCode)
        {
            switch (ColorCode)
            {
                case "#000000":
                case "#0053F7":
                case "#EA4532":
                case "#82F349":
                case "#74F5F5":
                case "#E759DE":
                case "#F2F851":

                    return true;

                default:

                    return false;
            }
        }

        public string PresetData() =>
            ColorCode + "," + ColorIntensity + "," + (BackgroundOnly ? 1 : 0);
    }
}