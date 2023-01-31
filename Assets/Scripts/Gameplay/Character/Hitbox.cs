﻿using UnityEngine;

namespace Gameplay.Character
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private VitalsPool _vitalsPool;

        public HitDetails TakeHit(float damage)
        {
            float damageDealt = 0;
            if (_vitalsPool != null)
                damageDealt = _vitalsPool.TakeDamage(damage);
            
            EntityOwner entityOwner = gameObject.layer switch
            {
                6 => EntityOwner.Player,
                8 => EntityOwner.Enemy,
                11 => EntityOwner.Neutral,
                _ => EntityOwner.Enviroment,
            };
            return new HitDetails(entityOwner, damageDealt);
        }
    }

    public readonly struct HitDetails
    {
        public float Damage { get; }
        public EntityOwner HitEntityOwner { get; }

        public HitDetails(EntityOwner hitEntityOwner, float damage)
        {
            HitEntityOwner = hitEntityOwner;
            Damage = damage;
        }
    }
}