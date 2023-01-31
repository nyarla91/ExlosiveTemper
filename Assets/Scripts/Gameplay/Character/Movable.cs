using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movable : LazyGetComponent<Rigidbody>
    {
        public Vector3 Velocity { get; set; }
        
        [Inject] private Pause Pause { get; set; }

        private void FixedUpdate()
        {
            Lazy.velocity = Pause.IsPaused ? Vector3.zero : Velocity;
        }
    }
}