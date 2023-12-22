// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;

namespace RoguelikeTDD
{
    [TestFixture]
    public class DungeonManagerTest
    {
        [Test]
        [CreateScene]
        public void DownStairs_新しいレベルで登り階段が初期位置()
        {
            var sut = new GameObject().AddComponent<DungeonManager>();
            // sut.DownStairs();
        }
    }
}
