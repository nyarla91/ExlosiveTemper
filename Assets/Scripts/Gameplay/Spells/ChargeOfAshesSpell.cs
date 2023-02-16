using Extentions;
using Gameplay.Character;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ChargeOfAshesSpell : SpellBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;

        private Timer _activatedDuration;

        private bool IsActive => _activatedDuration.IsOn;
        
        [Inject] private Pause Pause { get; set; }

        protected override void OnCast()
        {
            _activatedDuration.Restart();
        }

        private void Start()
        {
            _activatedDuration = new Timer(this, _duration, Pause);
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