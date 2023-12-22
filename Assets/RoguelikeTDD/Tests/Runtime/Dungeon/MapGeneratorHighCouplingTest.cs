// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace RoguelikeTDD.Dungeon
{
    /// <summary>
    /// ダンジョンマップ生成の高結合度テスト.
    /// ロジックでなく生成されたマップの特性を検証するテストを書いています
    /// </summary>
    [TestFixture]
    public partial class MapGeneratorTest
    {
        private const int RepeatCount = 10; // Note: 開発時は繰り返し回数を増やして検証。CIでは少なく。

        [Test]
        [Repeat(RepeatCount)]
        public void GenerateDungeonMap_マップの外周はすべて壁であること()
        {
            var map = MapGenerator.GenerateDungeonMap().Map;

            for (var y = 1; y < map.Length; y++)
            {
                Assert.That(map[y][0], Is.EqualTo(MapChip.Wall));
                Assert.That(map[y][^1], Is.EqualTo(MapChip.Wall));
            }

            for (var x = 0; x < map[0].Length; x++)
            {
                Assert.That(map[0][x], Is.EqualTo(MapChip.Wall));
                Assert.That(map[^1][x], Is.EqualTo(MapChip.Wall));
            }
        }

        [Test]
        [Repeat(RepeatCount)]
        public void GenerateDungeonMap_上り階段と下り階段がひとつづつ存在すること()
        {
            var map = MapGenerator.GenerateDungeonMap().Map;
            var upStairCount = 0;
            var downStairCount = 0;

            foreach (var row in map)
            {
                foreach (var mapChip in row)
                {
                    switch (mapChip)
                    {
                        case MapChip.UpStairs:
                            upStairCount++;
                            break;
                        case MapChip.DownStairs:
                            downStairCount++;
                            break;
                    }
                }
            }

            Assert.That(upStairCount, Is.EqualTo(1), nameof(upStairCount));
            Assert.That(downStairCount, Is.EqualTo(1), nameof(downStairCount));
        }

        // 再帰的に始点から終点まで移動できるルートを探索する
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static bool PathExists(MapChip[][] map, bool[][] visited, int startX, int startY, int endX, int endY)
        {
            if (startX == endX && startY == endY)
            {
                return true;
            }

            visited[startY][startX] = true;

            var mapHeight = map.Length;
            var mapWidth = map[0].Length;
            var destinations = new (int x, int y)[]
            {
                (startX, startY - 1), (startX, startY + 1), (startX - 1, startY), (startX + 1, startY) // 斜め移動はなし
            };

            foreach (var (x, y) in destinations)
            {
                if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                {
                    continue;
                }

                if (map[y][x] == MapChip.Wall) // 壁以外は移動可能
                {
                    continue;
                }

                if (visited[y][x])
                {
                    continue;
                }

                if (PathExists(map, visited, x, y, endX, endY))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool[][] CreateVisitedMap(MapChip[][] map)
        {
            var visited = new bool[map.Length][];
            for (var y = 0; y < map.Length; y++)
            {
                visited[y] = new bool[map[y].Length];
            }

            return visited;
        }

        [Test]
        [Repeat(RepeatCount)]
        public void GenerateDungeonMap_上り階段と下り階段の間を移動可能であること()
        {
            // Act
            var map = MapGenerator.GenerateDungeonMap().Map;
            var visited = CreateVisitedMap(map);
            var (upStairX, upStairY) = map.GetUpStairsPosition();
            var (downStairX, downStairY) = map.GetDownStairsPosition();

            // Verify
            var pathExists = PathExists(map, visited, upStairX, upStairY, downStairX, downStairY);
            Assert.That(pathExists, Is.True);
        }

        // TODO: 通路が他の通路や部屋と隣接して並走しないこと（ドアが連続しないこと）
    }
}
