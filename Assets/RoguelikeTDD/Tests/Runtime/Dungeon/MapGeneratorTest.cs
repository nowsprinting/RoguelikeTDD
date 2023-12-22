// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Linq;
using NUnit.Framework;
using RoguelikeTDD.TestUtils;

namespace RoguelikeTDD.Dungeon
{
    public partial class MapGeneratorTest
    {
        [TestCase(20, 10)]
        [TestCase(30, 20)]
        public void MapGenerator_指定サイズのマップ配列が生成されること(int width, int height)
        {
            // Act
            var sut = new MapGenerator(width, height);

            // Assert
            Assert.That(sut.Map.Length, Is.EqualTo(height));
            Assert.That(sut.Map, Has.All.Length.EqualTo(width));
        }

        [Test]
        public void MapGenerator_マップ配列が壁で埋められていること()
        {
            // Arrange
            int width = 20, height = 10;

            // Act
            var sut = new MapGenerator(width, height);

            // Assert
            var column = Enumerable.Repeat(MapChip.Wall, width).ToArray();
            var expected = Enumerable.Repeat(column, height).ToArray();
            Assert.That(sut.Map, Is.EqualTo(expected));
        }

        [Test]
        public void Write_マップを構成する要素をマップ配列に書き込めること()
        {
            // Arrange
            var sut = new MapGenerator(width: 10, height: 9);
            var rooms = new Room[] { new Room(1, 1, 3, 3), new Room(5, 5, 4, 3), };
            var passages = new Passage[] { new Passage(rooms[0], rooms[1]) };
            var doors = Door.CreateDoors(rooms[0], passages).Concat(Door.CreateDoors(rooms[1], passages)).ToArray();
            var stairs = new Stairs[]
            {
                new Stairs(new[] { new Room(1, 1, 1, 1) }, MapChip.UpStairs),
                new Stairs(new[] { new Room(6, 6, 1, 1) }, MapChip.DownStairs),
            };
            var expected = MapUtil.Parse(new[]
            {
                "WWWWWWWWWW", //
                "WURRWWWWWW", // 3x3の部屋、(0,0)に登り階段
                "WRRRDPPPWW", //
                "WRRRWWWPWW", //
                "WWWWWWWDWW", //
                "WWWWWRRRRW", // 4x3の部屋、(1,1)に降り階段
                "WWWWWRSRRW", //
                "WWWWWRRRRW", //
                "WWWWWWWWWW", //
            });

            // Act
            sut.Write(rooms, passages, doors, stairs);

            // Assert
            Assert.That(sut.Map, Is.EqualTo(expected));
        }
    }
}
