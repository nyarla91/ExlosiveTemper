using Extentions;
using Gameplay.Character;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ChargeOfAshesSpell : ContiniousSpell
    {
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;

        private void Start()
        {
            Player.Weapons.CurrentWeapon.HitscanBulletShot += CreateExplosion;
            Player.Weapons.SecondaryWeapon.HitscanBulletShot += CreateExplosion;
        }

        private void CreateExplosion(WeaponAttack attack, Vector3 point, Hitbox[] _)
        {
            if ( ! IsActive)
                return;
            LayerMask mask = LayerMask.GetMask("Player", "Enemy");
            AOE.GetTargets<Hitbox>(point, _explosionRadius, mask).Foreach(target => target?.TakeHit(_explosionDamage));
        }
    }
}