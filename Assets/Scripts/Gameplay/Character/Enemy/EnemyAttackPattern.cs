using Extentions;
using Extentions.Factory;
using Gameplay.Projectiles;
using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public abstract class EnemyAttackPattern : LazyGetComponent<EnemyComposition>
    {
        [SerializeField] private float _projectileBaseDamage;

        protected virtual float ProjectileDamage => _projectileBaseDamage;

        public PoolFactory ProjectileFactory { get; set; }

        protected void SpawnProjectile(Vector3 velocity) => SpawnProjectile(Transform.position.WithY(0.5f), velocity);
        protected void SpawnProjectile(Vector3 position, Vector3 velocity)
        {
            Projectile projectile = ProjectileFactory.GetNewObject<Projectile>(position);
            projectile.Init(EntityOwner.Enemy, ProjectileDamage, velocity);
            ProcessProjectile(projectile);
        }

        protected virtual void ProcessProjectile(Projectile projectile) { }
    }
}