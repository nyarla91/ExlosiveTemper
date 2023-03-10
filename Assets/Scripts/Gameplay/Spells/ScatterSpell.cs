using Extentions;
using Extentions.Factory;
using Gameplay.Character;
using Gameplay.VFX;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ScatterSpell : SpellBehaviour
    {
        [SerializeField] private GameObject _explosionrPrefab;
        [SerializeField] private float _damageRadius;
        [SerializeField] private float _damage;
        [SerializeField] private float _knockbackForce;

        public float DamageRadius => _damageRadius;
        
        [Inject] private ContainerFactory Factory { get; set; }

        protected override void OnCast()
        {
            Vector3 forward = Transform.forward;
            Vector3 aoePosition = Transform.position + forward * _damageRadius * 0.4f;
            LayerMask mask = LayerMask.GetMask("Enemy");
            AOE.GetTargets<Hitbox>(aoePosition, _damageRadius, mask).Foreach(target => target?.TakeHit(_damage));
            Player.Movable.AddKnockback( - forward * _knockbackForce);
        }
    }
}