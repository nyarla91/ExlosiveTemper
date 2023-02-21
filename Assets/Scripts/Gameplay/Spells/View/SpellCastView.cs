using Extentions;
using Extentions.Factory;
using Gameplay.VFX;
using UnityEngine;

namespace Gameplay.Spells.View
{
    public class SpellCastView : LazyGetComponent<SpellBehaviour>
    {
        [SerializeField] private AudioSource _castAudioSource;
        [SerializeField] private PoolFactory _vfxFactory;

        protected virtual Vector3 VFXPosition => Transform.position.WithY(1.5f);
        
        private void Awake()
        {
            Lazy.Casted += PlayCast;
        }

        private void PlayCast()
        {
            _castAudioSource?.PlayOneShot(_castAudioSource.clip);
            _vfxFactory?.GetNewObject<VisualEffectInstance>(VFXPosition);
        }
    }
}