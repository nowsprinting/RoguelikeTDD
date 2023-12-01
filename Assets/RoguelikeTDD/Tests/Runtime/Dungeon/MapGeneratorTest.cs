// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

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
    }
}
