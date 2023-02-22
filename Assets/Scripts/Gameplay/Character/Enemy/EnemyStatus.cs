using System;
using Extentions;
using Extentions.Factory;
using Gameplay.ImpactEffects;
using Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyStatus : LazyGetComponent<EnemyComposition>
    {
        [SerializeField] private float _deathShakeRatio;
        [SerializeField] private PoolFactory _damageEffectFactory;
        [SerializeField] private PoolFactory _deathEffectFactory;
        [SerializeField] private GameObject _healthbarPrefab;
        [SerializeField] private Transform _healthbarOrigin;

        private GameObject _droppedItem;
        
        [Inject] private ContainerFactory Factory { get; set; }
        [Inject] private Shake Shake { get; set; }
        
        public RectTransform HUD { get; set; }

        public void InitDroppedItem(GameObject prefab) => _droppedItem = prefab;

        private void Awake()
        {
            Lazy.VitalsPool.HealthIsOver += Die;
            Lazy.VitalsPool.TookDamage += PlayDamageEffect;
        }

        private void PlayDamageEffect(float _)
        {
            _damageEffectFactory.GetNewObject(Transform.position.WithY(1.5f));
        }

        private void Start()
        {
            FloatingHealthbar healthbar = Instantiate(_healthbarPrefab, HUD).GetComponent<FloatingHealthbar>();
            healthbar.Init(Lazy.VitalsPool.Health);
            healthbar.InitFloating(Camera.main, _healthbarOrigin, Lazy.VitalsPool);
        }

        private void Die()
        {
            _deathEffectFactory.GetNewObject(Transform.position.WithY(1.5f));
            Shake.AddImpulse(_deathShakeRatio);
            Factory.Instantiate<Transform>(_droppedItem, Transform.position);
            Destroy(gameObject);
        }
    }
}