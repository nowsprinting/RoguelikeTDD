// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using RoguelikeTDD.Dungeon;

namespace RoguelikeTDD.Hero
{
    public class HeroAction
    {
        public (int x, int y) NextPosition { get; private set; }

        public MapChip[][] Map { get; set; }

        public HeroAction(int x, int y)
        {
            NextPosition = (x, y);
        }

        private bool MoveTo(int x, int y)
        {
            if (Map[y][x].CanMove())
            {
                NextPosition = (x, y);
                return true;
            }

            return false;
        }

        public bool MoveLeft()
        {
            return MoveTo(NextPosition.x - 1, NextPosition.y);
        }

        public bool MoveRight()
        {
            return MoveTo(NextPosition.x + 1, NextPosition.y);
        }

        public bool MoveUp()
        {
            return MoveTo(NextPosition.x, NextPosition.y - 1);
        }

        public bool MoveDown()
        {
            return MoveTo(NextPosition.x, NextPosition.y + 1);
        }

        public bool MoveLeftUp()
        {
            return MoveTo(NextPosition.x - 1, NextPosition.y - 1);
        }

        public bool MoveRightUp()
        {
            return MoveTo(NextPosition.x + 1, NextPosition.y - 1);
        }

        public bool MoveLeftDown()
        {
            return MoveTo(NextPosition.x - 1, NextPosition.y + 1);
        }

        public bool MoveRightDown()
        {
            return MoveTo(NextPosition.x + 1, NextPosition.y + 1);
        }
    }
}
