using Extentions;
using Extentions.Factory;
using Gameplay.Projectiles;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class MeltSpell : SpellBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _meltRadius;
        
        [Inject] private ContainerFactory Factory { get; set; }
        
        public override void OnCast()
        {
            LayerMask mask = LayerMask.GetMask("EnemyAttack");
            Projectile[] projectiles = AOE.GetTargets<Projectile>(Transform.position, _meltRadius, mask);
            projectiles.Foreach(projectile => projectile.PoolDisable());
            Transform effect = Factory.Instantiate<Transform>(_effectPrefab, Transform.position.WithY(1.5f));
            effect.localScale = Vector3.one.WithY(1) * _meltRadius;
        }
    }
}