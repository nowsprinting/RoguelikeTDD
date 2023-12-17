// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public partial class RoomTest
    {
        [TestCase(0, 10, 9)]
        [TestCase(5, 15, 19)]
        public void Right_右端座標を返す(int x, int width, int right)
        {
            var room = new Room(x, 0, width, 0);
            Assert.That(room.Right, Is.EqualTo(right));
        }

        [TestCase(0, 10, 9)]
        [TestCase(5, 15, 19)]
        public void Bottom_下端座標を返す(int y, int height, int bottom)
        {
            var room = new Room(0, y, 0, height);
            Assert.That(room.Bottom, Is.EqualTo(bottom));
        }

        [TestCase(0, 0, 10, 8, 5, 4)]
        [TestCase(10, 10, 5, 3, 12, 11)]
        public void Center_中心座標を返す(int x, int y, int width, int height, int centerX, int centerY)
        {
            var room = new Room(x, y, width, height);
            Assert.That(room.Center, Is.EqualTo((centerX, centerY)));
        }
    }
}
