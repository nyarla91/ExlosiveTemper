using System;
using Extentions;
using UnityEngine;

namespace Gameplay.VFX
{
    public class Spin : Transformable
    {
        [SerializeField] private float _spinSpeed;

        private void Update()
        {
            Transform.Rotate(0, _spinSpeed * Time.deltaTime, 0);
        }
    }
}