using System.Linq;
using Extentions;
using Extentions.Factory;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    public class VortexGrenade : Projectile
    {
        [SerializeField] private GameObject _vortexEffect;
        [SerializeField] private float _vortexRadius;
        [SerializeField] private float _damage;
        [SerializeField] private float _pullForce;
        
        [Inject] private ContainerFactory Factory { get; set; }
        
        public override void PoolDisable()
        {
            LayerMask mask = LayerMask.GetMask("Player", "Enemy");
            Hitbox[] targets = AOE.GetTargets<Hitbox>(Transform.position, _vortexRadius, mask);
            Movable[] movables = targets.Select(target => target.GetComponent<Movable>()).ToArray();

            Transform vortex = Factory.Instantiate<Transform>(_vortexEffect, Transform.position.WithY(1.5f));
            vortex.localScale = Vector3.one * _vortexRadius;
            
            targets.Foreach(target => target?.TakeHit(_damage));
            movables.Foreach(movable => movable.AddKnockback(movable.Transform.DirectionTo(Transform.position).normalized * _pullForce));
            
            base.PoolDisable();
        }
    }
}