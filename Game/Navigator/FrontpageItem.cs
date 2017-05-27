using System.Data;

namespace AuroraEmu.Game.Navigator
{
    public class FrontpageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string Image { get; set; }
        public int Type { get; set; }
        public string Tag { get; set; }

        public FrontpageItem(DataRow row)
        {
            Id = (int)row["id"];
            Name = (string)row["name"];
            Description = (string)row["description"];
            Size = (int)row["size"];
            Image = (string)row["image"];
            Type = (int)row["type"];
            Tag = (string)row["tag"];
        }
    }
}