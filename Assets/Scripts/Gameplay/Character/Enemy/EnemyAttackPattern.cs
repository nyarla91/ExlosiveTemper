using Extentions;
using Extentions.Factory;
using Gameplay.Projectiles;
using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public abstract class EnemyAttackPattern<T> : LazyGetComponent<EnemyComposition> where T : Projectile
    {
        [SerializeField] private float _projectileBaseDamage;
        [SerializeField] private PoolFactory _projectileFactory;

        protected virtual float ProjectileDamage => _projectileBaseDamage;


        protected void SpawnProjectile(Vector3 velocity) => SpawnProjectile(Transform.position.WithY(0.5f), velocity);
        protected void SpawnProjectile(Vector3 position, Vector3 velocity)
        {
            T projectile = _projectileFactory.GetNewObject<T>(position);
            projectile.Init(EntityOwner.Enemy, ProjectileDamage, velocity);
            ProcessProjectile(projectile);
        }

        protected virtual void ProcessProjectile<TProjectile>(TProjectile projectile) { }
    }
}