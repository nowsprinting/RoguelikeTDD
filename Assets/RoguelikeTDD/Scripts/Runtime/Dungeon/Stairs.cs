// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Linq;
using UnityEngine;

namespace RoguelikeTDD.Dungeon
{
    public struct Stairs
    {
        public int X { get; }
        public int Y { get; }
        public Room Room { get; }
        public MapChip StairsType { get; }

        public Stairs(Room[] rooms, MapChip stairsType = MapChip.DownStairs)
        {
            Room = rooms[Random.Range(0, rooms.Length)];
            X = Random.Range(Room.X, Room.Right + 1);
            Y = Random.Range(Room.Y, Room.Bottom + 1);
            StairsType = stairsType;
        }

        public Stairs(Room[] rooms, Room ignoreRoom, MapChip stairsType = MapChip.DownStairs)
        {
            Room = rooms.Where(r => !r.Equals(ignoreRoom)).ToArray()[Random.Range(0, rooms.Length - 1)];
            X = Random.Range(Room.X, Room.Right + 1);
            Y = Random.Range(Room.Y, Room.Bottom + 1);
            StairsType = stairsType;
        }

        public void WriteToMap(MapChip[][] map)
        {
            map[Y][X] = StairsType;
        }
    }
}
