// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using RoguelikeTDD.Dungeon;
using RoguelikeTDD.TestUtils;

namespace RoguelikeTDD.Hero
{
    [TestFixture]
    public class HeroActionTest
    {
        public class 移動可能なケース
        {
            private readonly MapChip[][] _map = MapUtil.Parse(new[] { "RPD", "USR", "RRR", });
            private const int X = 1, Y = 1;

            [Test]
            public void MoveLeft_NextPositionにX座標マイナス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveLeft();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X - 1, Y)));
            }

            [Test]
            public void　MoveRight_NextPositionにX座標プラス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveRight();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X + 1, Y)));
            }

            [Test]
            public void MoveUp_NextPositionにY座標マイナス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveUp();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y - 1)));
            }

            [Test]
            public void MoveDown_NextPositionにY座標プラス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveDown();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y + 1)));
            }

            [Test]
            public void MoveLeftUp_NextPositionにX座標マイナス1Y座標マイナス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveLeftUp();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X - 1, Y - 1)));
            }

            [Test]
            public void MoveRightUp_NextPositionにX座標プラス1Y座標マイナス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveRightUp();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X + 1, Y - 1)));
            }

            [Test]
            public void MoveLeftDown_NextPositionにX座標マイナス1Y座標プラス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveLeftDown();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X - 1, Y + 1)));
            }

            [Test]
            public void MoveRightDown_NextPositionにX座標プラス1Y座標プラス1の座標が設定されること()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveRightDown();
                Assert.That(isMove, Is.True);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X + 1, Y + 1)));
            }
        }

        public class 移動できないケース
        {
            private readonly MapChip[][] _map = MapUtil.Parse(new[] { "WWW", "WRW", "WWW", });
            private const int X = 1, Y = 1;

            [Test]
            public void MoveLeft_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveLeft();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }

            [Test]
            public void MoveRight_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveRight();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }

            [Test]
            public void MoveUp_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveUp();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }

            [Test]
            public void MoveDown_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveDown();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }

            [Test]
            public void MoveLeftUp_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveLeftUp();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }

            [Test]
            public void MoveRightUp_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveRightUp();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }

            [Test]
            public void MoveLeftDown_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveLeftDown();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }

            [Test]
            public void MoveRightDown_戻り値がFalseかつNextPositionは変わらないこと()
            {
                var heroAction = new HeroAction(X, Y) { Map = _map };
                var isMove = heroAction.MoveRightDown();
                Assert.That(isMove, Is.False);
                Assert.That(heroAction.NextPosition, Is.EqualTo((X, Y)));
            }
        }
    }
}
