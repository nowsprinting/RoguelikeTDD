// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using RoguelikeTDD.Dungeon;
using RoguelikeTDD.Hero;
using UnityEngine;

namespace RoguelikeTDD
{
    public class DungeonManager : MonoBehaviour
    {
        private MapChip[][] _map;

        private GameState _state = new GameState(GameState.State.HeroIdle);
        private HeroController _hero;

        private void Awake()
        {
            _hero = new GameObject("Hero").AddComponent<HeroController>();
            _hero.GameState = _state;
        }

        private void NewLevel()
        {
            _map = MapGenerator.GenerateDungeonMap().Map;
            _map.Draw(this.gameObject);

            // Heroの初期位置を降り階段の位置に設定
            var heroPosition = _map.GetUpStairsPosition();
            _hero.Action = new HeroAction(heroPosition.x, heroPosition.y) { Map = _map };
            _hero.SetPositionToNextPosition();
        }

        private void Start()
        {
            NewLevel();
        }
    }
}
