using System;
using Extentions;
using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Projectiles
{
    public class Fireball : Projectile
    {
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;
        
        private void Awake()
        {
            HitboxHit += Explode;
        }

        private void Explode(Hitbox _)
        {
            LayerMask mask = LayerMask.GetMask("Player", "Enemy");
            Hitbox[] targets = AreaOfEffect.GetTargets(Transform.position, _explosionRadius, mask);
            targets.Foreach(hitbox => hitbox?.TakeHit(_explosionDamage));
        }
    }
}