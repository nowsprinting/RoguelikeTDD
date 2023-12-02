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
        public void GenerateMap_指定サイズのマップ配列が生成されること(
            int width, int height)
        {
            // Arrange
            var sut = new MapGenerator();

            // Act
            var map = sut.GenerateMap(width, height);

            // Assert
            Assert.That(map.Length, Is.EqualTo(width));
            Assert.That(map, Has.All.Length.EqualTo(height));
        }

        [Test]
        public void GenerateMap_マップ配列が壁で埋められていること()
        {
            // Arrange
            var width = 20;
            var height = 10;
            var sut = new MapGenerator();

            // Act
            var actual = sut.GenerateMap(width, height);

            // Assert
            var column = Enumerable.Repeat(MapChip.Wall, height).ToArray();
            var expected = Enumerable.Repeat(column, width).ToArray();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
