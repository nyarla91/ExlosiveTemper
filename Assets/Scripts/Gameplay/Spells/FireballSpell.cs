using Extentions;
using Extentions.Factory;
using Gameplay.Projectiles;
using UnityEngine;

namespace Gameplay.Spells
{
    public class FireballSpell : SpellBehaviour
    {
        [SerializeField] private float _projectileDamage;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private PoolFactory _projectileFactory;

        protected override void OnCast()
        {
            Fireball fireball = _projectileFactory.GetNewObject<Fireball>(Transform.position.WithY(1.5f) + Transform.forward * 2);
            fireball.Init(EntityOwner.Player, _projectileDamage, Transform.forward * _projectileSpeed);
        }
    }
}