// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public partial class PassageTest
    {
        private readonly Room[] _rooms = new[]
        {
            new Room(x: 0, y: 0, width: 2, height: 2), // 0: 左上
            new Room(x: 2, y: 0, width: 2, height: 2), // 1: 中央上
            new Room(x: 4, y: 0, width: 2, height: 2), // 2: 右上
            new Room(x: 0, y: 2, width: 2, height: 2), // 3: 左中央
            new Room(x: 2, y: 2, width: 2, height: 2), // 4: 中央
            new Room(x: 4, y: 2, width: 2, height: 2), // 5: 右中央
            new Room(x: 0, y: 4, width: 2, height: 2), // 6: 左下
            new Room(x: 2, y: 4, width: 2, height: 2), // 7: 中央下
            new Room(x: 4, y: 4, width: 2, height: 2), // 8: 右下
        };

        [Test]
        public void GetOuterPerimeter_外周の部屋を連結順に並べた配列が返ること()
        {
            // Arrange
            var expected = new[]
            {
                _rooms[0], _rooms[1], _rooms[2], _rooms[5], _rooms[8], _rooms[7], _rooms[6], _rooms[3], _rooms[0]
                // 外周順（添字0→1→2→5→8→7→6→3→0）
            };

            // Act
            var actual = Passage.GetOuterPerimeter(_rooms);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Repeat(100)]
        public void GetRandomCentralPassage_中央の部屋とランダムな部屋を通路でつなぐ配列が返ること()
        {
            // Arrange
            var center = _rooms[4];
            var centerTop = _rooms[1];
            var centerLeft = _rooms[3];
            var centerRight = _rooms[5];
            var centerBottom = _rooms[7];

            // Act
            var actual = Passage.GetRandomCentralPassage(_rooms);

            // Assert
            Assert.That(actual[0], Is.EqualTo(center));
            Assert.That(actual[1], Is.EqualTo(centerTop)
                .Or.EqualTo(centerLeft)
                .Or.EqualTo(centerRight)
                .Or.EqualTo(centerBottom));
        }
    }
}
