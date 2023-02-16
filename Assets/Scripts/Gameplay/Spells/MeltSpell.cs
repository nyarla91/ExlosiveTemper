using Extentions;
using Extentions.Factory;
using Gameplay.Projectiles;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class MeltSpell : SpellBehaviour
    {
        [SerializeField] private float _meltRadius;
        
        [Inject] private ContainerFactory Factory { get; set; }

        protected override void OnCast()
        {
            LayerMask mask = LayerMask.GetMask("EnemyAttack");
            Projectile[] projectiles = AOE.GetTargets<Projectile>(Transform.position, _meltRadius, mask);
            projectiles.Foreach(projectile => projectile.PoolDisable());
        }
    }
}