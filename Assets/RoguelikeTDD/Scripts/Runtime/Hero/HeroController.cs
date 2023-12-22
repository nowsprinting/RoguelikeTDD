// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using TestHelper.Input;
using UnityEngine;

namespace RoguelikeTDD.Hero
{
    [RequireComponent(typeof(TextMesh))]
    public class HeroController : MonoBehaviour
    {
        public HeroAction Action { get; internal set; } = new HeroAction(0, 0);
        public GameState GameState { get; internal set; } // DungeonManagerでnewしたものの参照を設定する
        internal IInput Input { private get; set; } = new InputWrapper();
        private TextMesh _textMesh;
        private (int x, int y) _position = (0, 0);
        private float _moveProgress = 0.0f;
        private const float MoveSpeed = 10.0f;

        private void Awake()
        {
            _textMesh = GetComponent<TextMesh>();
        }

        private void Start()
        {
            _textMesh.anchor = TextAnchor.LowerLeft;
            _textMesh.characterSize = 0.1f;
            _textMesh.fontSize = 90;
            _textMesh.text = "@";
        }

        public void SetPositionToNextPosition()
        {
            _position = Action.NextPosition;
            transform.position = new Vector3(_position.x, -_position.y, 0);
        }

        private void HeroIdol()
        {
            if (Input.GetKey(KeyCode.H))
            {
                if (Action.MoveLeft())
                {
                    GameState?.Next();
                }
            }
            else if (Input.GetKey(KeyCode.L))
            {
                if (Action.MoveRight())
                {
                    GameState?.Next();
                }
            }
            else if (Input.GetKey(KeyCode.K))
            {
                if (Action.MoveUp())
                {
                    GameState?.Next();
                }
            }
            else if (Input.GetKey(KeyCode.J))
            {
                if (Action.MoveDown())
                {
                    GameState?.Next();
                }
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                if (Action.MoveLeftUp())
                {
                    GameState?.Next();
                }
            }
            else if (Input.GetKey(KeyCode.U))
            {
                if (Action.MoveRightUp())
                {
                    GameState?.Next();
                }
            }
            else if (Input.GetKey(KeyCode.B))
            {
                if (Action.MoveLeftDown())
                {
                    GameState?.Next();
                }
            }
            else if (Input.GetKey(KeyCode.N))
            {
                if (Action.MoveRightDown())
                {
                    GameState?.Next();
                }
            }
        }

        private void HeroDoing()
        {
            if (_moveProgress < 1.0f)
            {
                _moveProgress += Time.deltaTime * MoveSpeed;
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(Action.NextPosition.x, -Action.NextPosition.y, 0), _moveProgress);
            }
            else
            {
                SetPositionToNextPosition();
                GameState?.Next();
                _moveProgress = 0.0f;
            }
        }

        private void Update()
        {
            switch (GameState?.CurrentState)
            {
                case GameState.State.HeroIdol:
                    HeroIdol();
                    break;
                case GameState.State.HeroDoing:
                    HeroDoing();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
