using System.Linq;
using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyMovement : LazyGetComponent<EnemyComposition>
    {
        [SerializeField] private bool _rotateTowardsPlayer;
        [SerializeField] private float _approachSpeed;
        [SerializeField] private float _retreatSpeed;
        [SerializeField] private float _preferedDistance;
        [SerializeField] [Tooltip("The less, the smoother acceleration")] private float _acceleration;

        private Vector3 _destination;

        private Movable _movable;

        public Movable Movable => _movable ??= GetComponent<Movable>();
        
        [Inject] private Pause Pause { get; set; }

        private void Update()
        {
            if (_rotateTowardsPlayer)
                Transform.rotation = Quaternion.LookRotation(Lazy.DirectionToPlayer, Vector3.up);
        }

        private void FixedUpdate()
        {
            if (Pause.IsPaused)
            {
                Movable.VoluntaryVelocity = Vector3.zero;
                return;
            }
            
            if (Lazy.Player != null)
                _destination = Lazy.Player.Transform.position;
            
            float distanceToPlayer = Vector3.Distance(Transform.position.WithY(0), _destination);
            float speed = GetSpeedFromDistance(distanceToPlayer);
            Vector3 targetVelocity = Lazy.DirectionToPlayer * speed;
            
            EnemyMovement[] nearbyEnemies = GetNearbyEnemies(8);
            if (nearbyEnemies.Length > 0)
            {
                EnemyMovement closestEnemy = nearbyEnemies.OrderBy
                    (enemy => Vector3.Distance(Transform.position, enemy.Transform.position)).ToArray()[0];
                targetVelocity += (Transform.position - closestEnemy.Transform.position).normalized * _retreatSpeed;
            }

            Movable.VoluntaryVelocity = Vector3.MoveTowards(Movable.VoluntaryVelocity, targetVelocity, Time.fixedDeltaTime * _acceleration);
        }

        private float GetSpeedFromDistance(float distanceToPlayer)
        {
            if (distanceToPlayer.ApproximatelyEqual(_preferedDistance, 0.2f))
                return 0;
            if (distanceToPlayer > _preferedDistance)
                return _approachSpeed;
            return -_retreatSpeed;
        }

        private EnemyMovement[] GetNearbyEnemies(float searchRadius)
        {
            Collider[] nearEnemiesColliders =
                Physics.OverlapSphere(Transform.position, searchRadius, LayerMask.GetMask("Enemy"));
            
            EnemyMovement[] nearEnemies = nearEnemiesColliders
                .Select(collider => collider.GetComponent<EnemyMovement>())
                .Where(enemy => enemy is not null)
                .Where(enemy => enemy != this).ToArray();
            
            return nearEnemies;
        }
    }
}