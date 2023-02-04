using System;
using Extentions;
using Extentions.Factory;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    public class Projectile : PooledObject
    {
        [SerializeField] [Tooltip("Set -1 to disable")] private float _maxTravelDistance = 10;
        [SerializeField] private bool _destroyedOnHit = true;

        private EntityOwner _owner;
        private Vector3 _direction;
        private float _speed;
        private float _distanceTraveled;

        private Rigidbody _rigidbody;
        private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        public Vector3 Direction
        {
            get => _direction;
            set
            {
                _direction = value.normalized;
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
            }
        }

        public Vector3 Velocity
        {
            get => Direction * Speed;
            set
            {
                Direction = value.normalized;
                Speed = value.magnitude;
            }
        }

        public EntityOwner Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                gameObject.layer = _owner switch
                {
                    EntityOwner.Player => 7,
                    EntityOwner.Enemy => 9,
                    EntityOwner.Neutral => 0,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        public float Damage { get; private set; }
        
        [Inject] private Pause Pause { get; set; }

        public event Action<Hitbox> HitboxHit;

        public void Init(EntityOwner owner, float damage, Vector3 velocity)
        {
            Owner = owner;
            Damage = damage;
            Velocity = velocity.WithY(0);
        }
        
        public override void PoolDisable()
        {
            Velocity = Vector3.zero;
            _distanceTraveled = 0;
            base.PoolDisable();
        }

        private void FixedUpdate()
        {
            Rigidbody.velocity = Pause.IsPaused ? Vector3.zero : (Speed * Direction);
            
            if (_maxTravelDistance.ApproximatelyEqual(-1, 0.001f))
                return;
            _distanceTraveled += Rigidbody.velocity.magnitude * Time.fixedDeltaTime;
            if (_distanceTraveled >= _maxTravelDistance)
                PoolDisable();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Hitbox hitbox))
                return;
            
            HitDetails hit = hitbox.TakeHit(Damage);
            HitboxHit?.Invoke(hitbox);
            if (_destroyedOnHit || hit.HitEntityOwner == EntityOwner.Enviroment)
                PoolDisable();
        }
    }
}