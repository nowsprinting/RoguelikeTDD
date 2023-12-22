// Copyright (c) 2023 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace RoguelikeTDD.Hero
{
    public class CameraController : MonoBehaviour
    {
        private HeroController _heroController;

        private void Start()
        {
            _heroController = FindObjectOfType<HeroController>();
        }

        private void Update()
        {
            var heroPosition = _heroController.transform.position;
            transform.position = new Vector3(heroPosition.x, heroPosition.y, -10f);
        }
    }
}
