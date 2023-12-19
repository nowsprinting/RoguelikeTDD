// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Text;
using RoguelikeTDD.Dungeon;

namespace RoguelikeTDD.TestUtils
{
    /// <summary>
    /// テストコードで使用する、MapChip配列と文字列の変換を行うユーティリティクラス.
    /// </summary>
    public static class MapUtil
    {
        /// <summary>
        /// 文字列配列をパースして、MapChip配列を作成する.
        /// </summary>
        /// <param name="input">1要素==1行で、内容はマップチップの頭文字（W,R,P,D,U,S）を並べたもの。なお、SはDownStairs.</param>
        /// <returns>MapChip配列</returns>
        public static MapChip[][] Parse(string[] input)
        {
            var map = new MapChip[input.Length][];
            for (var y = 0; y < input.Length; y++)
            {
                map[y] = new MapChip[input[y].Length];
                for (var x = 0; x < input[y].Length; x++)
                {
                    map[y][x] = input[y][x] switch
                    {
                        'W' => MapChip.Wall,
                        'R' => MapChip.Room,
                        'P' => MapChip.Passage,
                        'D' => MapChip.Door,
                        'U' => MapChip.UpStairs,
                        'S' => MapChip.DownStairs,
                        _ => MapChip.Wall
                    };
                }
            }

            return map;
        }

        /// <summary>
        /// MapChip配列から文字表現を作成する.
        /// </summary>
        /// <param name="map"></param>
        /// <returns>1要素==1行で、内容はマップチップの頭文字（W,R,P,D,U,S）を並べたもの。なお、SはDownStairs.</returns>
        public static string Dump(MapChip[][] map)
        {
            var output = new StringBuilder();
            foreach (var row in map)
            {
                foreach (var chip in row)
                {
                    output.Append(chip switch
                    {
                        MapChip.Wall => "W",
                        MapChip.Room => "R",
                        MapChip.Passage => "P",
                        MapChip.Door => "D",
                        MapChip.UpStairs => "U",
                        MapChip.DownStairs => "S",
                        _ => "W"
                    });
                }

                output.Append("\n");
            }

            return output.ToString(0, output.Length - 1);
        }
    }
}
