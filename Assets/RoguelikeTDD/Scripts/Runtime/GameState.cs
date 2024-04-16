// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

namespace RoguelikeTDD
{
    public class GameState
    {
        public enum State
        {
            HeroIdle,
            HeroDoing,
        }

        public State CurrentState { get; private set; } = State.HeroIdle;

        public GameState()
        {
        }

        public GameState(State state)
        {
            CurrentState = state;
        }

        public void Next()
        {
            CurrentState = CurrentState == State.HeroIdle ? State.HeroDoing : State.HeroIdle;
        }
    }
}
