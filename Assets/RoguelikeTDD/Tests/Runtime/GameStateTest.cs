// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace RoguelikeTDD
{
    [TestFixture]
    public class GameStateTest
    {
        [Test]
        public void GameState_初期状態はHeroIdleであること()
        {
            var gameState = new GameState();
            Assert.That(gameState.CurrentState, Is.EqualTo(GameState.State.HeroIdle));
        }

        [Test]
        public void Next_HeroIdle_HeroDoingに状態遷移すること()
        {
            var gameState = new GameState(GameState.State.HeroIdle);
            gameState.Next();
            Assert.That(gameState.CurrentState, Is.EqualTo(GameState.State.HeroDoing));
        }

        [Test]
        public void Next_HeroDoing_HeroIdleに状態遷移すること()
        {
            var gameState = new GameState(GameState.State.HeroDoing);
            gameState.Next();
            Assert.That(gameState.CurrentState, Is.EqualTo(GameState.State.HeroIdle));
        }
    }
}
