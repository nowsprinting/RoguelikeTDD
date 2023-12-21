// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;

namespace RoguelikeTDD.Dungeon
{
    public class MapGenerator
    {
        public MapChip[][] Map { get; }

        public MapGenerator(int width, int height)
        {
            Map = new MapChip[height][];
            for (var y = 0; y < height; y++)
            {
                Map[y] = new MapChip[width];
                for (var x = 0; x < width; x++)
                {
                    Map[y][x] = MapChip.Wall;
                }
            }
        }

        public void Write(Room[] rooms, Passage[] passages, Door[] doors, Stairs[] stairs)
        {
            foreach (var passage in passages)
            {
                passage.WriteToMap(Map);
            }

            foreach (var door in doors)
            {
                door.WriteToMap(Map);
            }

            foreach (var room in rooms)
            {
                room.WriteToMap(Map);
            }

            foreach (var stair in stairs)
            {
                stair.WriteToMap(Map);
            }
        }

        // すべての通路を生成し、配列で返す
        private static Passage[] CreateAllPassages(Room[] rooms)
        {
            var passages = new List<Passage>();
            var perimeter = Passage.GetOuterPerimeter(rooms);
            for (var i = 0; i < perimeter.Length - 1; i++)
            {
                passages.Add(new Passage(perimeter[i], perimeter[i + 1]));
            }

            var centralPassage = Passage.GetRandomCentralPassage(rooms);
            for (var i = 0; i < centralPassage.Length - 1; i++)
            {
                passages.Add(new Passage(centralPassage[i], centralPassage[i + 1]));
            }

            return passages.ToArray();
        }

        // すべての部屋のドアを生成し、配列で返す
        private static Door[] CreateAllDoors(Room[] room, Passage[] passages)
        {
            var doors = new List<Door>();
            foreach (var r in room)
            {
                doors.AddRange(Door.CreateDoors(r, passages));
            }

            return doors.ToArray();
        }

        /// <summary>
        /// ダンジョンのマップをランダムに生成して返す
        /// </summary>
        /// <returns></returns>
        public static MapGenerator GenerateDungeonMap()
        {
            var map = new MapGenerator(80, 24);
            var rooms = Room.CreateRooms(map.Map[0].Length, map.Map.Length, 3, 2);
            var passages = CreateAllPassages(rooms);
            var doors = CreateAllDoors(rooms, passages);
            var upStairs = new Stairs(rooms, MapChip.UpStairs);
            var downStairs = new Stairs(rooms, upStairs.Room, MapChip.DownStairs);
            map.Write(rooms, passages, doors, new[] { upStairs, downStairs });
            return map;
        }
    }
}
