using Extentions;
using Gameplay.ImpactEffects;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Gameplay.ScreenEffects
{
    public class ImpulseScreenEffect : LazyGetComponent<Volume>
    {
        [SerializeField] private float _pitchAmplitude;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _strength = 1;
        [SerializeField] [Range(0, 1)] private float _fadeSpeed = 0.1f;
        [SerializeField] [Range(0, 1)] private float _shakeRatio;


        [Inject] private Shake Shake { get; set; }
        
        protected void CreateImpulse() => CreateImpulse(1);
        protected void CreateImpulse(float impulse)
        {
            Shake.AddImpulse(impulse * _shakeRatio);
            _audioSource.pitch = Random.Range(1 - _pitchAmplitude, 1 + _pitchAmplitude);
            _audioSource.PlayOneShot(_audioSource.clip, impulse);
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