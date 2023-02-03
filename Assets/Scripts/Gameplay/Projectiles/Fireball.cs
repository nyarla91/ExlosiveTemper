using System;
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
            
        }
    }
}