// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using RoguelikeTDD.TestUtils;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public partial class PassageTest
    {
        [Test]
        public void Passage_2つの部屋の中心をつなぐ通路を生成できること()
        {
            // Arrange
            var from = new Room(x: 0, y: 0, width: 3, height: 3);
            var to = new Room(x: 4, y: 2, width: 4, height: 4);
            (int x, int y)[] expected =
            {
                (x: 1, y: 1), // 始点（fromの中心）
                (x: 2, y: 1), // x++（x座標を先に合わせる）
                (x: 3, y: 1), // x++
                (x: 4, y: 1), // x++
                (x: 5, y: 1), // x++
                (x: 6, y: 1), // x++
                (x: 6, y: 2), // y++（y座標を合わせる）
                (x: 6, y: 3), // y++
                (x: 6, y: 4), // 終点（toの中心）
            };

            // Act
            var actual = new Passage(from, to);

            // Assert
            Assert.That(actual.Steps, Is.EqualTo(expected));
        }

        [Test]
        public void Passage_2つの部屋の中心をつなぐ通路を生成できること_逆方向()
        {
            // Arrange
            var from = new Room(x: 4, y: 2, width: 4, height: 4);
            var to = new Room(x: 0, y: 0, width: 3, height: 3);
            (int x, int y)[] expected =
            {
                (x: 6, y: 4), // 始点（fromの中心）
                (x: 5, y: 4), // x--（x座標を先に合わせる）
                (x: 4, y: 4), // x--
                (x: 3, y: 4), // x--
                (x: 2, y: 4), // x--
                (x: 1, y: 4), // x--
                (x: 1, y: 3), // y--（y座標を合わせる）
                (x: 1, y: 2), // y--
                (x: 1, y: 1), // 終点（toの中心）
            };

            // Act
            var actual = new Passage(from, to);

            // Assert
            Assert.That(actual.Steps, Is.EqualTo(expected));
        }

        [Test]
        public void WriteToMap_通路をマップ配列に書き込めること()
        {
            // Arrange
            var from = new Room(x: 0, y: 0, width: 3, height: 3);
            var to = new Room(x: 4, y: 2, width: 4, height: 4);
            var map = new MapGenerator(width: 8, height: 6);
            var expected = MapUtil.Parse(new[]
            {
                "WWWWWWWW", // W:Wall
                "WPPPPPPW", // P:Passage
                "WWWWWWPW", //
                "WWWWWWPW", //
                "WWWWWWPW", //
                "WWWWWWWW", //
            });
            var passage = new Passage(from, to);

            // Act
            passage.WriteToMap(map.Map);

            // Assert
            Assert.That(map.Map, Is.EqualTo(expected));
        }

        [TestCase(1, 1)] // 始点
        [TestCase(2, 1)] // 中間点
        [TestCase(6, 4)] // 終点
        public void IsPointOnPassage_通路上の座標_Trueを返す(int x, int y)
        {
            // Arrange
            var from = new Room(x: 0, y: 0, width: 3, height: 3);
            var to = new Room(x: 4, y: 2, width: 4, height: 4);
            var passage = new Passage(from, to);

            // Act
            var actual = passage.IsPointOnPassage(x, y);

            // Assert
            Assert.That(actual, Is.True);
        }

        [TestCase(0, 0)] // 始点の左上
        public void IsPointOnPassage_通路外の座標_Falseを返す(int x, int y)
        {
            // Arrange
            var from = new Room(x: 0, y: 0, width: 3, height: 3);
            var to = new Room(x: 4, y: 2, width: 4, height: 4);
            var passage = new Passage(from, to);

            // Act
            var actual = passage.IsPointOnPassage(x, y);

            // Assert
            Assert.That(actual, Is.False);
        }
    }
}
