// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

namespace RoguelikeTDD.Dungeon
{
    public class MapGenerator
    {
        public int[][] Map { get; }

        public MapGenerator(int width, int height)
        {
            Map = new int[height][];
            for (var y = 0; y < height; y++)
            {
                Map[y] = new int[width];
            }
        }
    }
}
