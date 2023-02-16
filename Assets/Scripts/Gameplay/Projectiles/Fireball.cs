using System;
using Extentions;
using Extentions.Factory;
using Gameplay.Character;
using Gameplay.VFX;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    public class Fireball : Projectile
    {
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;
        
        [Inject] private ContainerFactory Factory { get; set; }
        
        private void Awake()
        {
            HitboxHit += Explode;
        }

        private void Explode(Hitbox _)
        {
            LayerMask mask = LayerMask.GetMask("Player", "Enemy");
            Hitbox[] targets = AOE.GetTargets<Hitbox>(Transform.position, _explosionRadius, mask);
            targets.Foreach(hitbox => hitbox?.TakeHit(_explosionDamage));
        }
    }
}