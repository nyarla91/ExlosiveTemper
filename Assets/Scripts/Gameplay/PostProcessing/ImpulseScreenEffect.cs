using Extentions;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gameplay.PostProcessing
{
    public class ImpulseScreenEffect : LazyGetComponent<Volume>
    {
        [SerializeField] private float _strength = 1;
        [SerializeField] [Range(0, 1)] private float _fadeSpeed = 0.1f;


        protected void CreateImpulse() => CreateImpulse(1);
        protected void CreateImpulse(float impulse)
        {
            impulse *= _strength;
            if (impulse < Lazy.weight)
                return;
            Lazy.weight = impulse;
        }
        
        private void FixedUpdate()
        {
            Lazy.weight *= (1 - _fadeSpeed);
        }
    }
}