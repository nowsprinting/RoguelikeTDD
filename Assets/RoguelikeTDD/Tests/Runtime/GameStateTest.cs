// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;

namespace RoguelikeTDD
{
    [TestFixture]
    public class GameStateTest
    {
        [Test]
        public void GameState_初期状態はHeroIdolであること()
        {
            var gameState = new GameState();
            Assert.That(gameState.CurrentState, Is.EqualTo(GameState.State.HeroIdol));
        }

        [Test]
        public void Next_HeroIdol_HeroDoingに状態遷移すること()
        {
            var gameState = new GameState(GameState.State.HeroIdol);
            gameState.Next();
            Assert.That(gameState.CurrentState, Is.EqualTo(GameState.State.HeroDoing));
        }

        [Test]
        public void Next_HeroDoing_HeroIdolに状態遷移すること()
        {
            var gameState = new GameState(GameState.State.HeroDoing);
            gameState.Next();
            Assert.That(gameState.CurrentState, Is.EqualTo(GameState.State.HeroIdol));
        }
    }
}
