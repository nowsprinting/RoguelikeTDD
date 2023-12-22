// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using RoguelikeTDD.Dungeon;
using UnityEngine;

namespace RoguelikeTDD
{
    public class DungeonManager : MonoBehaviour
    {
        private MapChip[][] _map;

        private void NewLevel()
        {
            _map = MapGenerator.GenerateDungeonMap().Map;
            _map.Draw(this.gameObject);
        }

        private void Start()
        {
            NewLevel();
        }
    }
}
