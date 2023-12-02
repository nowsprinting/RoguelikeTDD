// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

namespace RoguelikeTDD.Dungeon
{
    public class MapGenerator
    {
        public MapChip[][] GenerateMap(int width, int height)
        {
            var map = new MapChip[width][];
            for (var x = 0; x < width; x++)
            {
                map[x] = new MapChip[height];
                for (var y = 0; y < height; y++)
                {
                    map[x][y] = MapChip.Wall;
                }
            }

            return map;
        }
    }
}
