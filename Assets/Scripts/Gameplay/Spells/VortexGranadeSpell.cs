using Extentions;
using Extentions.Factory;
using Gameplay.Projectiles;
using UnityEngine;

namespace Gameplay.Spells
{
    public class VortexGranadeSpell : SpellBehaviour
    {
        [SerializeField] private PoolFactory _factory;
        [SerializeField] private float _projectileSpeed;
        
        public override void OnCast()
        {
            VortexGrenade grenade = _factory.GetNewObject<VortexGrenade>(Transform.position.WithY(1.5f) + Transform.forward);
            grenade.Init(EntityOwner.Player, 0, Transform.forward * _projectileSpeed);
        }
    }
}