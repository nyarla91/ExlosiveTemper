using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movable : LazyGetComponent<Rigidbody>
    {
        [SerializeField] [Range(0, 1)] private float _knockbackResistance;
        
        public Vector3 VoluntaryVelocity { get; set; }
        public Vector3 Knockback { get; private set; }
        
        [Inject] private Pause Pause { get; set; }

        public void AddKnockback(Vector3 knockback) => Knockback += knockback.WithY(0);

        private void FixedUpdate()
        {
            if (Pause.IsPaused)
            {
                Lazy.velocity = Vector3.zero;
                return;
            }

            Lazy.velocity = VoluntaryVelocity += Knockback;
            Knockback *= (1 - _knockbackResistance);
        }
    }
}