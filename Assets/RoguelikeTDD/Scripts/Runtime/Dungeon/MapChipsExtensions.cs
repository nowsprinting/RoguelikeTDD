// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace RoguelikeTDD.Dungeon
{
    public static class MapChipsExtensions
    {
        private const float MapChipSpritePixels = 64f;
        private static readonly Rect s_mapChipSpriteRect = new Rect(0, 0, MapChipSpritePixels, MapChipSpritePixels);

        public static void CreateSprite(this MapChip mapChip, int column, int row, GameObject parent = null)
        {
            var sprite = Sprite.Create(
                texture: Resources.Load<Texture2D>($"Sprites/{mapChip}"),
                rect: s_mapChipSpriteRect,
                pivot: Vector2.zero,
                pixelsPerUnit: MapChipSpritePixels
            );
            var spriteRenderer = new GameObject($"{mapChip}({column}, {row})").AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.transform.position = new Vector3(column, -row);
            if (parent)
            {
                spriteRenderer.transform.parent = parent.transform;
            }
        }

        public static void Draw(this MapChip[][] map, GameObject parent = null)
        {
            for (var y = 0; y < map.Length; y++)
            {
                var mapChips = map[y];
                for (var x = 0; x < mapChips.Length; x++)
                {
                    var mapChip = mapChips[x];
                    mapChip.CreateSprite(x, y, parent);
                }
            }
        }

        public static (int x, int y) GetUpStairsPosition(this MapChip[][] map)
        {
            for (var y = 0; y < map.Length; y++)
            {
                var mapChips = map[y];
                for (var x = 0; x < mapChips.Length; x++)
                {
                    var mapChip = mapChips[x];
                    if (mapChip == MapChip.UpStairs)
                    {
                        return (x, y);
                    }
                }
            }

            return (-1, -1);
        }

        public static (int x, int y) GetDownStairsPosition(this MapChip[][] map)
        {
            for (var y = 0; y < map.Length; y++)
            {
                var mapChips = map[y];
                for (var x = 0; x < mapChips.Length; x++)
                {
                    var mapChip = mapChips[x];
                    if (mapChip == MapChip.DownStairs)
                    {
                        return (x, y);
                    }
                }
            }

            return (-1, -1);
        }
    }
}
