// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

namespace RoguelikeTDD.Dungeon
{
    public class MapGenerator
    {
        public MapChip[][] Map { get; }

        public MapGenerator(int width, int height)
        {
            Map = new MapChip[height][];
            for (var y = 0; y < height; y++)
            {
                Map[y] = new MapChip[width];
                for (var x = 0; x < width; x++)
                {
                    Map[y][x] = MapChip.Wall;
                }
            }
        }
    }
}
