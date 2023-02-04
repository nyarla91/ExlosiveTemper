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
        
        public override void OnCast()
        {
            Debug.Log(_projectileFactory._prefab);
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = Random.insideUnitCircle.XYtoXZ() * 5;
            Fireball fireball = _projectileFactory.GetNewObject<Fireball>(Transform.position.WithY(1.5f) + Transform.forward);
            fireball.Init(EntityOwner.Player, _projectileDamage, Transform.forward * _projectileSpeed);
        }
    }
}