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
    }
}
