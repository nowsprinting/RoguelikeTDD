// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public partial class RoomTest
    {
        [TestCase(15, 12)]
        [TestCase(18, 15)]
        public void CreateRooms_部屋が9つ生成されること(int width, int height)
        {
            // Arrange
            int minRoomSize = 1, padding = 0;

            // Act
            var actual = Room.CreateRooms(width, height, minRoomSize, padding);

            // Assert
            Assert.That(actual, Has.Length.EqualTo(9));
        }

        [TestCase(0, 1, 1, 3, 2)]
        [TestCase(1, 6, 1, 9, 2)]
        [TestCase(2, 11, 1, 13, 2)]
        [TestCase(3, 1, 5, 3, 6)]
        [TestCase(4, 6, 5, 9, 6)]
        [TestCase(5, 11, 5, 13, 6)]
        [TestCase(6, 1, 9, 3, 11)]
        [TestCase(7, 6, 9, 9, 11)]
        [TestCase(8, 11, 9, 13, 11)]
        [Repeat(10)]
        public void CreateRooms_各部屋は9つの区画内に生成されること(int index, int left, int top, int right, int bottom)
        {
            // Arrange
            int width = 15, height = 12, minRoomSize = 2, padding = 1;

            // Act
            var actual = Room.CreateRooms(width, height, minRoomSize, padding);

            // Assert
            Assert.That(actual[index].X, Is.InRange(left, right));
            Assert.That(actual[index].Y, Is.InRange(top, bottom));
            Assert.That(actual[index].Right, Is.InRange(left, right));
            Assert.That(actual[index].Bottom, Is.InRange(top, bottom));
        }
    }
}
