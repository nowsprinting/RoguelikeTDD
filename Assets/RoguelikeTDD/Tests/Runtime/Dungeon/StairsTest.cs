// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Linq;
using NUnit.Framework;
using RoguelikeTDD.TestUtils;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public class StairsTest
    {
        [Test]
        [Repeat(10)]
        public void Stairs_ランダムな部屋のランダムな座標に階段を設置できること()
        {
            // Arrange
            var room1 = new Room(0, 0, 2, 2);
            var room2 = new Room(10, 10, 2, 2);
            var room3 = new Room(20, 20, 2, 2);
            var rooms = new[] { room1, room2, room3 };

            // Act
            var stairs = new Stairs(rooms);

            // Assert
            Assert.That(rooms, Does.Contain(stairs.Room)); // Note: 本来のactualとexpectedを逆に書いています
            Assert.That(stairs.X, Is.InRange(stairs.Room.X, stairs.Room.Right));
            Assert.That(stairs.Y, Is.InRange(stairs.Room.Y, stairs.Room.Bottom));
        }

        [Test]
        [Retry(100)]
        public void Stairs_選択肢の最小値が抽選されること()
        {
            // Arrange
            var room1 = new Room(0, 0, 2, 2);
            var room2 = new Room(10, 10, 2, 2);
            var rooms = new[] { room1, room2 };

            // Act
            var stairs = new Stairs(rooms);

            // Assert
            Assert.That(stairs.Room, Is.EqualTo(rooms[0]));
            Assert.That(stairs.X, Is.EqualTo(stairs.Room.Right));
            Assert.That(stairs.Y, Is.EqualTo(stairs.Room.Bottom));
        }

        [Test]
        [Retry(100)]
        public void Stairs_選択肢の最大値まで抽選されること()
        {
            // Arrange
            var room1 = new Room(0, 0, 2, 2);
            var room2 = new Room(10, 10, 2, 2);
            var rooms = new[] { room1, room2 };

            // Act
            var stairs = new Stairs(rooms);

            // Assert
            Assert.That(stairs.Room, Is.EqualTo(rooms.Last()));
            Assert.That(stairs.X, Is.EqualTo(stairs.Room.Right));
            Assert.That(stairs.Y, Is.EqualTo(stairs.Room.Bottom));
        }

        [Test]
        [Repeat(10)]
        public void Stairs_抽選から除外する部屋を指定_除外指定した部屋は抽選されない()
        {
            // Arrange
            var room1 = new Room(0, 0, 2, 2);
            var room2 = new Room(10, 10, 2, 2);
            var room3 = new Room(20, 20, 2, 2);
            var rooms = new[] { room1, room2, room3 };

            // Act
            var stairs = new Stairs(rooms: rooms, ignoreRoom: room2);

            // Assert
            Assert.That(stairs.Room, Is.EqualTo(room1).Or.EqualTo(room3));
        }

        [Test]
        public void WriteToMap_登り階段をマップに書き込めること()
        {
            // Arrange
            var room = new Room(1, 1, 1, 1); // (1,1)にしか階段はできない
            var stairsType = MapChip.UpStairs; // 登り階段
            var stairs = new Stairs(new[] { room }, stairsType);
            var map = new MapGenerator(width: 2, height: 2);
            var expected = MapUtil.Parse(new[]
            {
                "WW", // W:Wall
                "WU", // U:UpStairs
            });

            // Act
            stairs.WriteToMap(map.Map);

            // Assert
            Assert.That(map.Map, Is.EqualTo(expected));
        }

        [Test]
        public void WriteToMap_降り階段をマップに書き込めること()
        {
            // Arrange
            var room = new Room(1, 1, 1, 1); // (1,1)にしか階段はできない
            var stairsType = MapChip.DownStairs; // 降り階段
            var stairs = new Stairs(new[] { room }, stairsType);
            var map = new MapGenerator(width: 2, height: 2);
            var expected = MapUtil.Parse(new[]
            {
                "WW", // W:Wall
                "WS", // S:DownStairs
            });

            // Act
            stairs.WriteToMap(map.Map);

            // Assert
            Assert.That(map.Map, Is.EqualTo(expected));
        }
    }
}
