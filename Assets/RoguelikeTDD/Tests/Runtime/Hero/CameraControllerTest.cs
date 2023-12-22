// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace RoguelikeTDD.Hero
{
    [TestFixture]
    public class CameraControllerTest
    {
        [Test]
        public async Task Update_HeroControllerの座標に追随すること()
        {
            // Arrange
            int targetX = 3, targetY = 2;
            var hero = new GameObject().AddComponent<HeroController>();
            hero.GameState = new GameState(GameState.State.HeroIdol);
            var camera = new GameObject().AddComponent<CameraController>();

            // Act
            hero.transform.position = new Vector3(targetX, targetY, 0);
            await UniTask.NextFrame(PlayerLoopTiming.LastUpdate);

            // Assert
            var expected = new Vector3(targetX, targetY, -10f);
            Assert.That(camera.transform.position, Is.EqualTo(expected));
        }
    }
}
