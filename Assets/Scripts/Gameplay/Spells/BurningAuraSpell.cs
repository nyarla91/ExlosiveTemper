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
                print(_radius);
                LayerMask mask = LayerMask.GetMask("Enemy");
                Collider[] enemiesInside = Physics.OverlapSphere(Transform.position.WithY(1.5f), _radius, mask);
                Hitbox[] hitboxes = enemiesInside.Select(enemy => enemy.GetComponent<Hitbox>()).ToArray();
                float damage = _damagePerSecond * _tickPeriod;
                foreach (Hitbox hitbox in hitboxes)
                {
                    hitbox.TakeHit(damage);
                }
                _radius -= _radiusReductionPerSecond * _tickPeriod;
                yield return new PausableWaitForSeconds(this, Pause, _tickPeriod);
            }
        }
    }
}