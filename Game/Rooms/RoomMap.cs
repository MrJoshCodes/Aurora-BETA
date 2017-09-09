using AuroraEmu.Game.Items;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Rooms
{
    public class RoomMap
    {
        public string Name { get; }
        public int DoorX { get; }
        public int DoorY { get; }
        public double DoorZ { get; }
        public int DoorRotation { get; }
        public string RawMap { get; }

        private string[] _splitMap;

        public (int, int) MapSize { get; }

        public bool[,] PassableTiles { get; }
        public double[,] TileHeights { get; }

        public char[,] HeightMap { get; }

        public string RelativeHeightMap { get; private set; }
        public bool DisableDiagonal { get; }

        public RoomMap(MySqlDataReader reader)
        {
            Name = reader.GetString("name");
            DoorX = reader.GetInt32("door_x");
            DoorY = reader.GetInt32("door_y");
            DoorZ = reader.GetDouble("door_z");
            DoorRotation = reader.GetInt32("door_rotation");
            RawMap = reader.GetString("raw_map");

            _splitMap = RawMap.Split('|');

            MapSize = (_splitMap[0].Length, _splitMap.Length);

            PassableTiles = new bool[MapSize.Item1, MapSize.Item2];
            TileHeights = new double[MapSize.Item1, MapSize.Item2];
            HeightMap = new char[MapSize.Item1, MapSize.Item2];

            Generate();
            DisableDiagonal = false;
        }

        public void Generate()
        {
            for (int y = 0; y < MapSize.Item2; y++)
            {
                //if (y > 0)
                //    splitMap[y] = splitMap[y].Substring(1); 

                for (int x = 0; x < MapSize.Item1; x++)
                {
                    string square = _splitMap[y].Substring(x, 1).Trim().ToLower();

                    if (x == DoorX && y == DoorY)
                        square = (int) DoorZ + "";

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

                RelativeHeightMap += (char) 13;
            }
        }
    }
}