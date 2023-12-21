// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;

namespace RoguelikeTDD.Dungeon
{
    public struct Door
    {
        public int X { get; }
        public int Y { get; }

        public Door(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Door[] CreateDoors(Room room, Passage[] passages)
        {
            var doors = new List<Door>();
            var left = room.X - 1;
            var top = room.Y - 1;
            var right = room.Right + 1;
            var bottom = room.Bottom + 1;
            for (var y = top; y <= bottom; y++)
            {
                for (var x = left; x <= right; x++)
                {
                    // 角にはドアは作らない
                    if (x == left && y == top || x == right && y == top || x == left && y == bottom ||
                        x == right && y == bottom)
                    {
                        continue;
                    }

                    if (x == left || x == right || y == top || y == bottom)
                    {
                        foreach (var passage in passages)
                        {
                            if (passage.IsPointOnPassage(x, y))
                            {
                                doors.Add(new Door(x, y));
                            }
                        }
                    }
                }
            }

            return doors.ToArray();
        }

        public void WriteToMap(MapChip[][] map)
        {
            map[Y][X] = MapChip.Door;
        }
    }
}
