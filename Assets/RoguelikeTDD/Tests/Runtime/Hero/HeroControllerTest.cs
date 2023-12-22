// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using RoguelikeTDD.Dungeon;
using RoguelikeTDD.TestDoubles;
using RoguelikeTDD.TestUtils;
using TestHelper.Attributes;
using UnityEngine;

namespace RoguelikeTDD.Hero
{
    [TestFixture]
    public class HeroControllerTest
    {
        [Test]
        [CreateScene(camera: true)]
        [TakeScreenshot]
        public void Start_プレイヤーキャラクターが表示されること()
        {
            new GameObject().AddComponent<HeroController>();
        }

        public class HeroIdol
        {
            public class 移動可能なケース
            {
                private readonly MapChip[][] _map = MapUtil.Parse(new[] { "RPD", "USR", "RRR", });
                private const int X = 1, Y = 1;

                [TestCase(KeyCode.H, -1, 0)]
                [TestCase(KeyCode.L, 1, 0)]
                [TestCase(KeyCode.K, 0, -1)]
                [TestCase(KeyCode.J, 0, 1)]
                [TestCase(KeyCode.Y, -1, -1)]
                [TestCase(KeyCode.U, 1, -1)]
                [TestCase(KeyCode.B, -1, 1)]
                [TestCase(KeyCode.N, 1, 1)]
                public async Task Update_HJKLYUBNキーを入力_移動先のHeroActionが設定されること(KeyCode key, int moveX, int moveY)
                {
                    var sut = new GameObject().AddComponent<HeroController>();
                    sut.Action = new HeroAction(X, Y) { Map = _map };
                    sut.GameState = new GameState(GameState.State.HeroIdol);
                    sut.Input = new StubInput { PushedKeys = new[] { key } };
                    await UniTask.NextFrame(PlayerLoopTiming.LastUpdate);
                    sut.Input = new StubInput();

                    Assert.That(sut.Action.NextPosition, Is.EqualTo((X + moveX, Y + moveY)));
                }

                [TestCase(KeyCode.H)]
                [TestCase(KeyCode.L)]
                [TestCase(KeyCode.K)]
                [TestCase(KeyCode.J)]
                [TestCase(KeyCode.Y)]
                [TestCase(KeyCode.U)]
                [TestCase(KeyCode.B)]
                [TestCase(KeyCode.N)]
                public async Task Update_HJKLYUBNキーを入力_HeroDoingに状態遷移すること(KeyCode key)
                {
                    var sut = new GameObject().AddComponent<HeroController>();
                    sut.Action = new HeroAction(X, Y) { Map = _map };
                    sut.GameState = new GameState(GameState.State.HeroIdol);
                    sut.Input = new StubInput { PushedKeys = new[] { key } };
                    await UniTask.NextFrame(PlayerLoopTiming.LastUpdate);
                    sut.Input = new StubInput();

                    Assert.That(sut.GameState.CurrentState, Is.EqualTo(GameState.State.HeroDoing));
                }
            }

            public class 移動できないケース
            {
                private readonly MapChip[][] _map = MapUtil.Parse(new[] { "WWW", "WRW", "WWW", });
                private const int X = 1, Y = 1;

                [TestCase(KeyCode.H)]
                [TestCase(KeyCode.L)]
                [TestCase(KeyCode.K)]
                [TestCase(KeyCode.J)]
                [TestCase(KeyCode.Y)]
                [TestCase(KeyCode.U)]
                [TestCase(KeyCode.B)]
                [TestCase(KeyCode.N)]
                public async Task Update_HJKLYUBNキーを入力_移動されないこと(KeyCode key)
                {
                    var sut = new GameObject().AddComponent<HeroController>();
                    sut.Action = new HeroAction(X, Y) { Map = _map };
                    sut.GameState = new GameState(GameState.State.HeroIdol);
                    sut.Input = new StubInput { PushedKeys = new[] { key } };
                    await UniTask.NextFrame(PlayerLoopTiming.LastUpdate);
                    sut.Input = new StubInput();

                    Assert.That(sut.Action.NextPosition, Is.EqualTo((X, Y)));
                }

                [TestCase(KeyCode.H)]
                [TestCase(KeyCode.L)]
                [TestCase(KeyCode.K)]
                [TestCase(KeyCode.J)]
                [TestCase(KeyCode.Y)]
                [TestCase(KeyCode.U)]
                [TestCase(KeyCode.B)]
                [TestCase(KeyCode.N)]
                public async Task Update_HJKLYUBNキーを入力_状態遷移されないこと(KeyCode key)
                {
                    var sut = new GameObject().AddComponent<HeroController>();
                    sut.Action = new HeroAction(X, Y) { Map = _map };
                    sut.GameState = new GameState(GameState.State.HeroIdol);
                    sut.Input = new StubInput { PushedKeys = new[] { key } };
                    await UniTask.NextFrame(PlayerLoopTiming.LastUpdate);
                    sut.Input = new StubInput();

                    Assert.That(sut.GameState.CurrentState, Is.EqualTo(GameState.State.HeroIdol));
                }
            }
        }

        public class HeroDoing
        {
            [Test]
            [Timeout(5000)]
            public async Task Update_HeroDoingのときHeroが移動かつHeroIdolに状態遷移すること()
            {
                int targetX = 1, targetY = 0;
                var sut = new GameObject().AddComponent<HeroController>();
                sut.GameState = new GameState(GameState.State.HeroDoing);
                sut.Action = new HeroAction(targetX, targetY);
                await sut.GameState.WaitState(GameState.State.HeroIdol); // HeroIdolに遷移するまで待つ

                Assert.That(sut.transform.position, Is.EqualTo(new Vector3(targetX, targetY, 0)));
            }
        }
    }
}
