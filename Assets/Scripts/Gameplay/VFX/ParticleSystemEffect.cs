using System.Collections;
using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.VFX
{
    public class ParticleSystemEffect : Transformable
    {
        [SerializeField] private float _duration;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _soundVariants;
        [SerializeField] [Range(0, 1)] private float _pitchAmplitude = 0.15f;
        [SerializeField] private AnimationCurve _scaleVolumeCurve;
        [SerializeField] [Range(0, 1)] private float _fadeOutSpeed;

        private bool _stopped;
        private float _volumeMultiplier = 1;

        public bool Play { get; set; } = true;
        [Inject] private Pause Pause { get; set; }

        private void Start()
        {
            if (_duration >= 0)
                StartCoroutine(Lifetime());
            if (_audioSource != null)
            {
                _audioSource.pitch = Random.Range(1 - _pitchAmplitude, 1 + _pitchAmplitude);
                _audioSource.PlayOneShot(_soundVariants.PickRandomElement());
            }
        }

        private void Update()
        {
            if (Pause.IsPaused)
            {
                _particleSystem.Pause(false);
            }
            else if (_stopped || !Play)
            {
                _volumeMultiplier *= 1 - _fadeOutSpeed;
                _particleSystem.Stop(false);
            }
            else
            {
                _particleSystem.Play(false);
            }

            if (_audioSource != null)
            {
                float scale = Transform.localScale.magnitude;
                _audioSource.volume = _scaleVolumeCurve.Evaluate(scale) * _volumeMultiplier;
            }
        }

        private IEnumerator Lifetime()
        {
            yield return new PausableWaitForSeconds(this, Pause, _duration);
            _stopped = true;
            yield return new PausableWaitForSeconds(this, Pause, 5);
            Destroy(gameObject);
        }
    }
}