// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using RoguelikeTDD.TestUtils;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public class DoorTest
    {
        [Test]
        public void CreateDoors_部屋に隣接した通路があるとき_ドアを生成()
        {
            // Arrange
            var room = new Room(x: 1, y: 1, width: 3, height: 3);
            var room2 = new Room(x: 5, y: 2, width: 1, height: 1); // (4,2)に通路を通すための部屋
            var passages = new Passage[] { new Passage(room, room2) };
            var expected = new Door[] { new Door(x: 4, y: 2) };

            // Act
            var actual = Door.CreateDoors(room, passages); // 部屋の外周を走査して、隣接した通路の座標にドアを作る

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CreateDoors_部屋の上下左右に隣接した通路があるとき_ドアを複数生成()
        {
            // Arrange
            var room = new Room(x: 1, y: 1, width: 3, height: 3);
            var passageHorizontal = new Passage(
                new Room(x: 0, y: 2, width: 1, height: 1),
                new Room(x: 4, y: 2, width: 1, height: 1));　// 横方向の通路
            var passageVertical = new Passage(
                new Room(x: 2, y: 0, width: 1, height: 1),
                new Room(x: 2, y: 4, width: 1, height: 1));　// 縦方向の通路
            var passages = new Passage[] { passageHorizontal, passageVertical };
            var expected = new Door[]
            {
                new Door(x: 0, y: 2), new Door(x: 4, y: 2), new Door(x: 2, y: 0), new Door(x: 2, y: 4),
            };

            // Act
            var actual = Door.CreateDoors(room, passages);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void CreateDoors_部屋の角に隣接した通路があるとき_ドアは生成されない()
        {
            // Arrange
            var room = new Room(x: 1, y: 1, width: 3, height: 3);
            var passageLeftTop = new Passage(
                new Room(x: 0, y: 0, width: 1, height: 1),
                new Room(x: 0, y: 0, width: 1, height: 1));
            var passageRightTop = new Passage(
                new Room(x: 4, y: 0, width: 1, height: 1),
                new Room(x: 4, y: 0, width: 1, height: 1));
            var passageLeftBottom = new Passage(
                new Room(x: 0, y: 4, width: 1, height: 1),
                new Room(x: 0, y: 4, width: 1, height: 1));
            var passageRightBottom = new Passage(
                new Room(x: 4, y: 4, width: 1, height: 1),
                new Room(x: 4, y: 4, width: 1, height: 1));
            var passages = new Passage[] { passageLeftTop, passageRightTop, passageLeftBottom, passageRightBottom };
            var expected = new Door[] { };

            // Act
            var actual = Door.CreateDoors(room, passages);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void WriteToMap_ドアをマップに書き込めること()
        {
            // Arrange
            var door = new Door(x: 1, y: 1);
            var map = new MapGenerator(width: 2, height: 2);
            var expected = MapUtil.Parse(new[]
            {
                "WW", // W:Wall
                "WD", // D:Door
            });

            // Act
            door.WriteToMap(map.Map);

            //Assert
            Assert.That(map.Map, Is.EqualTo(expected));
        }
    }
}
