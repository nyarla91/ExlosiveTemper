using Extentions;
using UnityEngine;

namespace Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movable : LazyGetComponent<Rigidbody>
    {
        public Vector3 Velocity { get; set; }

        private void FixedUpdate()
        {
            Lazy.velocity = Velocity;
        }
    }
}