// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;

namespace RoguelikeTDD.Dungeon
{
    [TestFixture]
    public class MapChipsExtensionsTest
    {
        [Test]
        [CreateScene(camera: true)]
        [TakeScreenshot]
        public void CreateSprite_MapChipに応じたSpriteが表示されること([Values] MapChip mapChip)
        {
            mapChip.CreateSprite(0, 0);
        }

        [Test]
        [CreateScene(camera: true)]
        [TakeScreenshot]
        public void Draw_MapChipの二次元配列に応じたSpriteが表示されること()
        {
            var map = new[]
            {
                new[] { MapChip.Wall, MapChip.Room, MapChip.Passage },
                new[] { MapChip.Door, MapChip.UpStairs, MapChip.DownStairs },
            };
            map.Draw();
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
