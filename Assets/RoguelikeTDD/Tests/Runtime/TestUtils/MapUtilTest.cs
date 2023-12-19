// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using RoguelikeTDD.Dungeon;

namespace RoguelikeTDD.TestUtils
{
    [TestFixture]
    public class MapUtilTest
    {
        [Test]
        public void Parse_文字列配列からMapChip配列が作られること()
        {
            var input = new[]
            {
                "WRP", // Wall, Room, Passage 
                "DUS", // Door, UpStairs, DownStairs
            };
            var expected = new[]
            {
                new[] { MapChip.Wall, MapChip.Room, MapChip.Passage },
                new[] { MapChip.Door, MapChip.UpStairs, MapChip.DownStairs },
            };
            var actual = MapUtil.Parse(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Dump_MapChip配列から文字列が作られること()
        {
            var map = new[]
            {
                new[] { MapChip.Wall, MapChip.Room, MapChip.Passage },
                new[] { MapChip.Door, MapChip.UpStairs, MapChip.DownStairs },
            };
            var expected = "WRP\nDUS";
            var actual = MapUtil.Dump(map);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
