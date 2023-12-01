// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

namespace RoguelikeTDD.Dungeon
{
    public class MapGenerator
    {
        public int[][] GenerateMap(int width, int height)
        {
            var map = new int[width][];
            for (var x = 0; x < width; x++)
            {
                map[x] = new int[height];
            }

            return map;
        }
    }
}
