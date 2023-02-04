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

        private bool _stopped;

        public bool Play { get; set; } = true;
        [Inject] private Pause Pause { get; set; }

        private void Start()
        {
            if (_duration >= 0)
                StartCoroutine(Lifetime());
        }

        private void Update()
        {
            if (Pause.IsPaused)
                _particleSystem.Pause(false);
            else if (_stopped || ! Play)
                _particleSystem.Stop(false);
            else
                _particleSystem.Play(false);
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