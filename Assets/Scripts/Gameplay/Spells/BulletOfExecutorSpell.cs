using Gameplay.Character;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Spells
{
    public class BulletOfExecutorSpell : ContiniousSpell
    {
        [SerializeField] private float _maxDuration;
        [SerializeField] [Range(0, 1)] private float _maxHealthPercent;

        private void Start()
        {
            Player.Weapons.CurrentWeapon.HitscanBulletShot += TryExecute;
        }

        private void TryExecute(WeaponAttack attack, Vector3 point, Hitbox[] hitboxes)
        {
            if ( ! IsActive)
                return;
            Interrupt();
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