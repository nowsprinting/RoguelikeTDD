// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public partial class RoomTest
    {
        [Test]
        public void WriteToMap_部屋の位置をマップ配列に書き込めること()
        {
            // Arrange
            int mapWidth = 4, mapHeight = 2;
            int roomX = 1, roomY = 0, roomWidth = 2, roomHeight = 1;
            var expected = new MapChip[][]
            {
                new[] { MapChip.Wall, MapChip.Room, MapChip.Room, MapChip.Wall },
                new[] { MapChip.Wall, MapChip.Wall, MapChip.Wall, MapChip.Wall },
            };
            var map = new MapGenerator(mapWidth, mapHeight);
            var room = new Room(roomX, roomY, roomWidth, roomHeight);

            // Act
            room.WriteToMap(map.Map);

            // Assert
            Assert.That(map.Map, Is.EqualTo(expected));
        }
    }
}
