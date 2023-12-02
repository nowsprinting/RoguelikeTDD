// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Linq;
using NUnit.Framework;

namespace RoguelikeTDD.Dungeon
{
    public class MapGeneratorTest
    {
        [TestCase(20, 10)]
        [TestCase(30, 20)]
        public void MapGenerator_指定サイズのマップ配列が生成されること(int width, int height)
        {
            var sut = new MapGenerator(width, height);

            // Assert
            Assert.That(sut.Map.Length, Is.EqualTo(height));
            Assert.That(sut.Map, Has.All.Length.EqualTo(width));
        }

        [Test]
        public void MapGenerator_マップ配列が壁で埋められていること()
        {
            int width = 20, height = 10;
            var sut = new MapGenerator(width, height);

            var column = Enumerable.Repeat(MapChip.Wall, width).ToArray();
            var expected = Enumerable.Repeat(column, height).ToArray();
            Assert.That(sut.Map, Is.EqualTo(expected));
        }
    }
}
