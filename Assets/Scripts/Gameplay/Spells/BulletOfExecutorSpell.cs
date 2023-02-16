using System;
using Extentions;
using Gameplay.Character;
using Gameplay.VFX;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class BulletOfExecutorSpell : SpellBehaviour
    {
        [SerializeField] private float _maxDuration;
        [SerializeField] [Range(0, 1)] private float _maxHealthPercent;
        
        private Timer _activatedDuration;

        private bool IsActive => _activatedDuration.IsOn;
        
        [Inject] private Pause Pause { get; set; }

        protected override void OnCast()
        {
            _activatedDuration.Restart();
        }

        private void Start()
        {
            _activatedDuration = new Timer(this, _maxDuration, Pause);
            Player.Weapons.CurrentWeapon.HitscanBulletShot += TryExecute;
        }

        private void TryExecute(WeaponAttack attack, Vector3 point, Hitbox[] hitboxes)
        {
            if ( ! IsActive)
                return;
            _activatedDuration.Reset();
            foreach (Hitbox hitbox in hitboxes)
            {
                VitalsPool vitals = hitbox.GetComponent<VitalsPool>();
                if (vitals == null)
                    continue;
                if (vitals.Health.Percent <= _maxHealthPercent)
                {
                    vitals.TakeDamage(10000);
                }
            }
        }
    }
}