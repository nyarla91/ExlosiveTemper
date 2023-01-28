using UnityEngine;

namespace Gameplay.Character
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private VitalsPool _vitalsPool;

        public HitDetails TakeHit(float damage)
        {
            _vitalsPool?.TakeDamage(damage);
            
            EntityOwner entityOwner = gameObject.layer switch
            {
                6 => EntityOwner.Player,
                8 => EntityOwner.Enemy,
                _ => EntityOwner.Neutral
            };
            return new HitDetails(entityOwner);
        }
    }

    public readonly struct HitDetails
    {
        public EntityOwner HitEntityOwner { get; }

        public HitDetails(EntityOwner hitEntityOwner)
        {
            HitEntityOwner = hitEntityOwner;
        }
    }
}