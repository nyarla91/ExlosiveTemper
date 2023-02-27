using Extentions;
using Extentions.Factory;
using Gameplay.ImpactEffects;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyStatus : LazyGetComponent<EnemyComposition>
    {
        [SerializeField] private float _deathShakeRatio;
        [SerializeField] private PoolFactory _damageEffectFactory;
        [SerializeField] private PoolFactory _deathEffectFactory;
        [SerializeField] private Transform _healthbarOrigin;

        private GameObject _droppedItem;

        public Transform HealthbarOrigin => _healthbarOrigin;
        
        [Inject] private ContainerFactory Factory { get; set; }
        [Inject] private Shake Shake { get; set; }

        public void InitDroppedItem(GameObject prefab) => _droppedItem = prefab;

        private void Awake()
        {
            Lazy.Vitals.HealthIsOver += Die;
            Lazy.Vitals.TookDamage += PlayDamageEffect;
        }

        private void PlayDamageEffect(float _)
        {
            _damageEffectFactory.GetNewObject(Transform.position.WithY(1.5f));
        }

        private void Die()
        {
            _deathEffectFactory.GetNewObject(Transform.position.WithY(1.5f));
            Shake.AddImpulse(_deathShakeRatio);
            
            if (_droppedItem != null)
                Factory.Instantiate<Transform>(_droppedItem, Transform.position);
            
            Destroy(gameObject);
        }
    }
}