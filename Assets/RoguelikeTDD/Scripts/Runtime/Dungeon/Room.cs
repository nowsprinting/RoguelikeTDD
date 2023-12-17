// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace RoguelikeTDD.Dungeon
{
    public struct Room
    {
        public int X { get; private set; } // 部屋の左上のX座標
        public int Y { get; private set; } // 部屋の左上のY座標
        public int Width { get; private set; } // 部屋の幅
        public int Height { get; private set; } // 部屋の高さ
        public int Right => X + Width - 1; // 部屋の右端のX座標
        public int Bottom => Y + Height - 1; // 部屋の下端のY座標
        public (int x, int y) Center => (X + Width / 2, Y + Height / 2); // 部屋の中心座標

        public Room(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public static Room CreateRoomInBounds(int left, int top, int right, int bottom, int minRoomSize = 1,
            int padding = 0)
        {
            var roomMaxWidth = right - left + 1 - padding * 2; // パディングを考慮
            var roomMaxHeight = bottom - top + 1 - padding * 2;
            var roomLeft = left + padding;
            var roomTop = top + padding;
            var roomRight = right - padding;
            var roomBottom = bottom - padding;

            var width = Random.Range(minRoomSize, roomMaxWidth + 1);
            var height = Random.Range(minRoomSize, roomMaxHeight + 1);
            var x = Random.Range(roomLeft, roomRight - width + 2);
            var y = Random.Range(roomTop, roomBottom - height + 2);
            return new Room(x, y, width, height);
        }

        public void WriteToMap(MapChip[][] map)
        {
            for (var y = Y; y <= Bottom; y++)
            {
                for (var x = X; x <= Right; x++)
                {
                    map[y][x] = MapChip.Room;
                }
            }
        }

        public static Room[] CreateRooms(int width, int height, int minRoomSize = 1, int padding = 0)
        {
            var rooms = new Room[9];
            var roomWidth = width / 3;
            var roomHeight = height / 3;
            var roomIndex = 0;
            for (var y = 0; y < 3; y++)
            {
                var top = y * roomHeight;
                var bottom = top + roomHeight - 1;
                for (var x = 0; x < 3; x++)
                {
                    var left = x * roomWidth;
                    var right = left + roomWidth - 1;
                    rooms[roomIndex] = CreateRoomInBounds(left, top, right, bottom, minRoomSize, padding);
                    roomIndex++;
                }
            }

            return rooms;
        }
    }
}
