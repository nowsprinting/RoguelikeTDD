// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using TestHelper.Attributes;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public class MapChipsExtensionsTest
    {
        [Test]
        [CreateScene(camera: true)]
        [TakeScreenshot]
        [GameViewResolution(GameViewResolution.VGA)]
        public void CreateSprite_MapChipに応じたSpriteが表示されること([Values] MapChip mapChip)
        {
            mapChip.CreateSprite(0, 0);
        }

        [Test]
        [CreateScene(camera: true)]
        [TakeScreenshot]
        [GameViewResolution(GameViewResolution.VGA)]
        public void Draw_MapChipの二次元配列に応じたSpriteが表示されること()
        {
            var map = new[]
            {
                new[] { MapChip.Wall, MapChip.Room, MapChip.Passage },
                new[] { MapChip.Door, MapChip.UpStairs, MapChip.DownStairs },
            };
            map.Draw();
        }

        [TestCase(MapChip.Wall, false)]
        [TestCase(MapChip.Room, true)]
        [TestCase(MapChip.Passage, true)]
        [TestCase(MapChip.Door, true)]
        [TestCase(MapChip.UpStairs, true)]
        [TestCase(MapChip.DownStairs, true)]
        public void CanMove_移動可能なMapChipにはTrueが返ること(MapChip mapChip, bool expected)
        {
            var actual = mapChip.CanMove();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetUpStairsPosition_登り階段の座標が返ること()
        {
            var map = new[]
            {
                new[] { MapChip.Wall, MapChip.Room, MapChip.Passage },
                new[] { MapChip.Door, MapChip.UpStairs, MapChip.DownStairs },
            };
            var (x, y) = map.GetUpStairsPosition();
            Assert.That(x, Is.EqualTo(1));
            Assert.That(y, Is.EqualTo(1));
        }

        [Test]
        public void GetDownStairsPosition_降り階段の座標が返ること()
        {
            var map = new[]
            {
                new[] { MapChip.Wall, MapChip.Room, MapChip.Passage },
                new[] { MapChip.Door, MapChip.UpStairs, MapChip.DownStairs },
            };
            var (x, y) = map.GetDownStairsPosition();
            Assert.That(x, Is.EqualTo(2));
            Assert.That(y, Is.EqualTo(1));
        }
    }
}
