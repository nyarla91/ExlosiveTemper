using Extentions;
using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Spells
{
    public class ScatterSpell : SpellBehaviour
    {
        [SerializeField] private float _damageRadius;
        [SerializeField] private float _damage;
        [SerializeField] private float _knockbackForce;
        
        public override void OnCast()
        {
            Vector3 forward = Transform.forward;
            Vector3 aoePosition = Transform.position + forward * _damageRadius * 0.4f;
            LayerMask mask = LayerMask.GetMask("Enemy");
            AOE.GetTargets<Hitbox>(aoePosition, _damageRadius, mask).Foreach(target => target?.TakeHit(_damage));
            
            Player.Movable.AddKnockback( - forward * _knockbackForce);
        }
    }
}