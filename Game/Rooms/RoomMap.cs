using System.Data;

namespace AuroraEmu.Game.Rooms
{
    public class RoomMap
    {
        public string Name { get; private set; }
        public int DoorX { get; private set; }
        public int DoorY { get; private set; }
        public double DoorZ { get; private set; }
        public int DoorRotation { get; private set; }
        public string RawMap { get; private set; }

        private string[] splitMap;

        public (int,int) MapSize { get; private set; }

        public bool[,] PassableTiles { get; private set; }
        public double[,] TileHeights { get; private set; }

        public char[,] HeightMap { get; private set; }

        public string RelativeHeightMap { get; private set; }

        public RoomMap(DataRow row)
        {
            Name = (string)row["name"];
            DoorX = (int)row["door_x"];
            DoorY = (int)row["door_y"];
            DoorZ = (double)row["door_z"];
            DoorRotation = (int)row["door_rotation"];
            RawMap = (string)row["raw_map"];

            splitMap = RawMap.Split('|');

            MapSize = (splitMap[0].Length, splitMap.Length);

            PassableTiles = new bool[MapSize.Item1, MapSize.Item2];
            TileHeights = new double[MapSize.Item1, MapSize.Item2];
            HeightMap = new char[MapSize.Item1, MapSize.Item2];

            Generate();
        }

        public void Generate()
        {
            for (int y = 0; y < MapSize.Item2; y++)
            {
                //if (y > 0)
                //    splitMap[y] = splitMap[y].Substring(1); 

                for (int x = 0; x < MapSize.Item1; x++)
                {
                    string square = splitMap[y].Substring(x, 1).Trim().ToLower();

                    if (x == DoorX && y == DoorY)
                        square = (int)DoorZ + "";

                    if (double.TryParse(square, out double height))
                    {
                        PassableTiles[x, y] = true;
                        TileHeights[x, y] = height;
                    }
                    else
                    {
                        PassableTiles[x, y] = false;
                        TileHeights[x, y] = 0;
                    }

                    HeightMap[x, y] = square[0];
                    RelativeHeightMap += square;
                }

                RelativeHeightMap += (char)13;
            }
        }
    }
}
