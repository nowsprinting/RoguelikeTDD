// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using NUnit.Framework;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public class RoomTest
    {
        [TestCase(0, 0, 19, 9)]
        [TestCase(20, 10, 39, 19)]
        [Repeat(100)]
        public void CreateRoomInBounds_部屋が指定区画内に作られること(int left, int top, int right, int bottom)
        {
            // Arrange
            var roomMaxWidth = right - left + 1;
            var roomMaxHeight = bottom - top + 1;

            // Act
            var room = Room.CreateRoomInBounds(left, top, right, bottom);

            // Assert
            Assert.That(room.X, Is.InRange(left, right));
            Assert.That(room.Y, Is.InRange(top, bottom));
            Assert.That(room.Width, Is.LessThanOrEqualTo(roomMaxWidth));
            Assert.That(room.Height, Is.LessThanOrEqualTo(roomMaxHeight));
        }

        [TestCase(0, 0, 19, 9, 1)]
        [TestCase(20, 10, 39, 19, 5)]
        [Repeat(100)]
        public void CreateRoomInBounds_最小サイズ指定_部屋が指定区画内に作られること(
            int left, int top, int right, int bottom, int minRoomSize)
        {
            // Arrange
            var roomMaxWidth = right - left + 1;
            var roomMaxHeight = bottom - top + 1;

            // Act
            var room = Room.CreateRoomInBounds(left, top, right, bottom, minRoomSize);

            // Assert
            Assert.That(room.X, Is.InRange(left, right));
            Assert.That(room.Y, Is.InRange(top, bottom));
            Assert.That(room.Width, Is.InRange(minRoomSize, roomMaxWidth));
            Assert.That(room.Height, Is.InRange(minRoomSize, roomMaxHeight));
        }

        [TestCase(0, 0, 19, 9, 0)]
        [TestCase(20, 10, 39, 19, 2)]
        [Repeat(100)]
        public void CreateRoomInBounds_パディング指定_部屋が指定区画内に作られること(
            int left, int top, int right, int bottom, int padding)
        {
            // Arrange
            var minRoomSize = 1; // 固定値
            var roomMaxWidth = right - left + 1 - padding * 2; // パディングを考慮
            var roomMaxHeight = bottom - top + 1 - padding * 2;
            var roomLeft = left + padding;
            var roomTop = top + padding;
            var roomRight = right - padding;
            var roomBottom = bottom - padding;

            // Act
            var room = Room.CreateRoomInBounds(left, top, right, bottom, minRoomSize, padding);

            // Assert
            Assert.That(room.X, Is.InRange(roomLeft, roomRight));
            Assert.That(room.Y, Is.InRange(roomTop, roomBottom));
            Assert.That(room.Width, Is.InRange(minRoomSize, roomMaxWidth));
            Assert.That(room.Height, Is.InRange(minRoomSize, roomMaxHeight));
        }

        [Test]
        public void CreateRoomInBounds_部屋の位置とサイズがランダムであること()
        {
            // Arrange
            int left = 0, top = 0, right = 19, bottom = 9; // 固定値

            // Act
            var xDistribution = new HashSet<int>();
            var yDistribution = new HashSet<int>();
            var widthDistribution = new HashSet<int>();
            var heightDistribution = new HashSet<int>();
            for (var i = 0; i < 100; i++)
            {
                var room = Room.CreateRoomInBounds(left, top, right, bottom);
                xDistribution.Add(room.X);
                yDistribution.Add(room.Y);
                widthDistribution.Add(room.Width);
                heightDistribution.Add(room.Height);
            }

            // Assert
            Assert.That(xDistribution, Has.Count.GreaterThan(1), "x座標が1パターンしか生成されていない");
            Assert.That(yDistribution, Has.Count.GreaterThan(1), "y座標が1パターンしか生成されていない");
            Assert.That(widthDistribution, Has.Count.GreaterThan(1), "幅が1パターンしか生成されていない");
            Assert.That(heightDistribution, Has.Count.GreaterThan(1), "高さが1パターンしか生成されていない");
        }

        [Test]
        public void CreateRoomInBounds_指定した最大サイズの部屋が作られること()
        {
            // Arrange
            int left = 0, top = 0, right = 4, bottom = 4, minRoomSize = 2, padding = 1; // 固定値
            var expected = new HashSet<Room>
            {
                new Room(1, 1, 2, 2),
                new Room(2, 1, 2, 2),
                new Room(1, 2, 2, 2),
                new Room(2, 2, 2, 2),
                new Room(1, 1, 3, 2),
                new Room(1, 2, 3, 2),
                new Room(1, 1, 2, 3),
                new Room(2, 1, 2, 3),
                new Room(1, 1, 3, 3),
            };

            // Act
            var roomDistribution = new HashSet<Room>();
            for (var i = 0; i < 100; i++)
            {
                var room = Room.CreateRoomInBounds(left, top, right, bottom, minRoomSize, padding);
                roomDistribution.Add(room);
            }

            // Assert
            Assert.That(roomDistribution, Is.EquivalentTo(expected));
        }
    }
}
