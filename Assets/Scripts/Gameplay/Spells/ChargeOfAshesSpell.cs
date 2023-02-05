using System;
using Extentions;
using Extentions.Factory;
using Gameplay.Character;
using Gameplay.VFX;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ChargeOfAshesSpell : SpellBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private ParticleSystemEffect[] _activatedEffect;
        [SerializeField] private float _duration;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;

        private Timer _activatedDuration;

        private bool IsActive => _activatedDuration.IsOn;
        
        [Inject] private Pause Pause { get; set; }
        [Inject] private ContainerFactory Factory { get; set; }
        
        public override void OnCast()
        {
            _activatedDuration.Restart();
            PlaySound();
        }

        private void Start()
        {
            _activatedDuration = new Timer(this, _duration, Pause);
            Player.Weapons.CurrentWeapon.HitscanBulletShot += CreateExplosion;
            Player.Weapons.SecondaryWeapon.HitscanBulletShot += CreateExplosion;
        }

        private void Update()
        {
            _activatedEffect.Foreach(effect => effect.Play = IsActive);
        }

        private void CreateExplosion(WeaponAttack attack, Vector3 point, Hitbox[] _)
        {
            if ( ! IsActive)
                return;
            LayerMask mask = LayerMask.GetMask("Player", "Enemy");
            AOE.GetTargets<Hitbox>(point, _explosionRadius, mask).Foreach(target => target?.TakeHit(_explosionDamage));
            Explosion.CreateExplosion(Factory, _explosionPrefab, point, _explosionRadius);
        }
    }
}