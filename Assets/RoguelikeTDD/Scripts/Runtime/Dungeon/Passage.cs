// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using Random = UnityEngine.Random;

namespace RoguelikeTDD.Dungeon
{
    public class Passage
    {
        public (int x, int y)[] Steps { get; }

        public Passage(Room from, Room to)
        {
            var fromCenter = from.Center;
            var toCenter = to.Center;
            var x = fromCenter.x;
            var y = fromCenter.y;
            var distance = Math.Abs(fromCenter.x - toCenter.x) + Math.Abs(fromCenter.y - toCenter.y) + 1;
            var steps = new (int x, int y)[distance];
            var stepIndex = 0;
            steps[stepIndex++] = (x, y);
            while (x != toCenter.x)
            {
                x += x < toCenter.x ? 1 : -1;
                steps[stepIndex++] = (x, y);
            }

            while (y != toCenter.y)
            {
                y += y < toCenter.y ? 1 : -1;
                steps[stepIndex++] = (x, y);
            }

            Steps = steps;
        }

        public void WriteToMap(MapChip[][] map)
        {
            foreach (var (x, y) in Steps)
            {
                map[y][x] = MapChip.Passage;
            }
        }

        public bool IsPointOnPassage(int x, int y)
        {
            foreach (var (px, py) in Steps)
            {
                if (px == x && py == y)
                {
                    return true;
                }
            }

            return false;
        }

        // 3*3に配置されている部屋を外周順に並べて返す
        public static Room[] GetOuterPerimeter(Room[] rooms)
        {
            var perimeter = new Room[rooms.Length];
            perimeter[0] = rooms[0];
            perimeter[1] = rooms[1];
            perimeter[2] = rooms[2];
            perimeter[3] = rooms[5];
            perimeter[4] = rooms[8];
            perimeter[5] = rooms[7];
            perimeter[6] = rooms[6];
            perimeter[7] = rooms[3];
            perimeter[8] = rooms[0];
            return perimeter;
        }

        public static Room[] GetRandomCentralPassage(Room[] rooms)
        {
            var center = rooms[4];
            var centerTop = rooms[1];
            var centerLeft = rooms[3];
            var centerRight = rooms[5];
            var centerBottom = rooms[7];

            var random = Random.value;
            if (random < 0.25f)
            {
                return new[] { center, centerTop };
            }

            if (random < 0.5f)
            {
                return new[] { center, centerLeft };
            }

            if (random < 0.75f)
            {
                return new[] { center, centerRight };
            }

            return new[] { center, centerBottom };
        }
    }
}
