using Extentions;
using UnityEngine;
using UnityEngine.VFX;

namespace Gameplay.Spells.View
{
    public class BurningAuraSpellView : LazyGetComponent<BurningAuraSpell>
    {
        [SerializeField] private AudioSource _auraSound;
        [SerializeField] [Range(0, 1)] private float _volume;
        [SerializeField] [Range(0, 1)] private float _volumeLerpSpeed;
        [SerializeField] private VisualEffect _auraEffect;
        [SerializeField] private float _particlesPerRadius;
        
        private void Update()
        {
            bool emit = Lazy.Radius > 0;
            _auraSound.volume = Mathf.Lerp(_auraSound.volume, emit ? _volume : 0, _volumeLerpSpeed);
            if (emit)
            {
                _auraEffect.Play();
            }
            else
            {
                _auraEffect.Stop();
                return;
            }
            _auraEffect.SetFloat("Radius", Lazy.Radius);
            _auraEffect.SetFloat("Particles", Lazy.Radius * _particlesPerRadius);
        }
    }
}