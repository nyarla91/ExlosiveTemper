using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyMovement : LazyGetComponent<EnemyComposition>
    {
        [SerializeField] private float _approachSpeed;
        [SerializeField] private float _retreatSpeed;
        [SerializeField] private float _preferedDistance;
        [SerializeField] [Tooltip("The less, the smoother acceleration")] private float _acceleration;

        private Transform _player;
        private Vector3 _destination;

        private Movable _movable;

        public Movable Movable => _movable ??= GetComponent<Movable>();
        
        [Inject] private Pause Pause { get; set; }
        
        [Inject]
        private void Construct(PlayerMovement player)
        {
            _player = player.Transform;
        }

        private void FixedUpdate()
        {
            if (Pause.IsPaused)
            {
                Movable.Velocity = Vector3.zero;
                return;
            }

            if (_player != null)
                _destination = _player.position;
            float distanceToPlayer = Vector3.Distance(Transform.position.WithY(0), _destination);
            
            float speed;
            if (distanceToPlayer.ApproximatelyEqual(_preferedDistance,0.2f))
                speed = 0;
            else if (distanceToPlayer > _preferedDistance)
                speed = _approachSpeed;
            else
                speed = -_retreatSpeed;

            Vector3 targetVelocity = Lazy.DirectionToPlayer * speed; 
            Movable.Velocity = Vector3.MoveTowards(Movable.Velocity, targetVelocity, Time.fixedDeltaTime * _acceleration);
        }
    }
}