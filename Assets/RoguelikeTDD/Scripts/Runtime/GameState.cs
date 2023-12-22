// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

namespace RoguelikeTDD
{
    public class GameState
    {
        public enum State
        {
            HeroIdol,
            HeroDoing,
        }

        public State CurrentState { get; private set; } = State.HeroIdol;

        public GameState()
        {
        }

        public GameState(State state)
        {
            CurrentState = state;
        }

        public void Next()
        {
            CurrentState = CurrentState == State.HeroIdol ? State.HeroDoing : State.HeroIdol;
        }
    }
}
