// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Linq;
using TestHelper.Input;
using UnityEngine;

namespace RoguelikeTDD.TestDoubles
{
    public class StubInput : InputWrapper
    {
        public KeyCode[] PushedKeys { get; set; } = Array.Empty<KeyCode>();

        public override bool GetKey(KeyCode key)
        {
            return PushedKeys.Contains(key);
        }
    }
}
