using Extentions;
using Extentions.Factory;
using Gameplay.VFX;
using UnityEngine;

namespace Gameplay.Projectiles
{
    public class ProjectileImpactView : LazyGetComponent<Projectile>
    {
        [SerializeField] private PoolFactory _vfxFactory;

        private void Awake()
        {
            Lazy.PoolDisabled += SpawnVFX;
        }

        private void SpawnVFX(PooledObject _)
        {
            _vfxFactory.GetNewObject<VisualEffectInstance>(Transform.position);
        }
    }
}