using System;
using System.Collections;
using Extentions;
using Gameplay.Character;
using Gameplay.VFX;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class BurningAuraSpell : SpellBehaviour
    {
        [SerializeField] private AudioSource _auraSource;
        [SerializeField] private AnimationCurve _auraVolumeCurve;
        [SerializeField] private ParticleSystemEffect _effect;
        [SerializeField] private float _damagePerSecond;
        [SerializeField] private float _tickPeriod;
        [SerializeField] private float _radiusReductionPerSecond;
        [SerializeField] private float _radiusPerCast;
        [SerializeField] private float _healingRadiusMultiplier;
        
        private float _radius;
        
        [Inject] private Pause Pause { get; set; }
        
        public override void OnCast()
        {
            _radius += _radiusPerCast;
            PlaySound();
        }

        private void Start()
        {
            StartCoroutine(DealingDamage());
            Player.Vitals.Health.GainedExcees += excess => _radius += excess * _healingRadiusMultiplier;
        }

        private void Update()
        {
            _effect.Play = _radius > 0;
            _effect.Transform.localScale = new Vector3(_radius, 1, _radius);
            _auraSource.volume = _auraVolumeCurve.Evaluate(_radius);
        }

        private IEnumerator DealingDamage()
        {
            while (true)
            {
                print(_radius);
                LayerMask mask = LayerMask.GetMask("Enemy");
                Hitbox[] targets = AOE.GetTargets<Hitbox>(Transform.position, _radius, mask);
                float damage = _damagePerSecond * _tickPeriod;
                targets.Foreach(target => target?.TakeHit(damage));
                _radius = Mathf.Max(_radius - _radiusReductionPerSecond * _tickPeriod, 0);
                yield return new PausableWaitForSeconds(this, Pause, _tickPeriod);
            }
        }
    }
}