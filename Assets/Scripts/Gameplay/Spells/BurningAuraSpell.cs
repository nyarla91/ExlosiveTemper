using System;
using System.Collections;
using System.Linq;
using Extentions;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class BurningAuraSpell : SpellBehaviour
    {
        [SerializeField] private float _damagePerSecond;
        [SerializeField] private float _tickPeriod;
        [SerializeField] private float _radiusReductionPerSecond;
        [SerializeField] private float _radiusPerCast;
        [SerializeField] private float _healingRadiusModifier;
        
        private float _radius;
        
        [Inject] private Pause Pause { get; set; }
        
        public override void OnCast()
        {
            _radius += _radiusPerCast;
            
        }

        private void Start()
        {
            StartCoroutine(DealingDamage());
        }

        private IEnumerator DealingDamage()
        {
            while (true)
            {
                Hitbox[] targets = AreaOfEffect.GetTargets(Transform.position, _radius, LayerMask.GetMask("Enemy"));
                float damage = _damagePerSecond * _tickPeriod;
                targets.Foreach(target => target.TakeHit(damage));
                _radius = Mathf.Max(_radius - _radiusReductionPerSecond * _tickPeriod, 0);
                yield return new PausableWaitForSeconds(this, Pause, _tickPeriod);
            }
        }
    }
}