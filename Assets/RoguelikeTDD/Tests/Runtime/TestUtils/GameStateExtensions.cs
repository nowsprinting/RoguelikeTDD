// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace RoguelikeTDD.TestUtils
{
    public static class GameStateExtensions
    {
        public static async Task WaitState(this GameState gameState, GameState.State state)
        {
            await UniTask.WaitUntil(() => gameState.CurrentState == state);
        }
    }
}
