using System;
using Extentions;
using Extentions.Factory;
using Gameplay.PostProcessing;
using Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyStatus : LazyGetComponent<EnemyComposition>
    {
        [SerializeField] private float _deathShakeRatio;
        [SerializeField] private GameObject _damagePrefab;
        [SerializeField] private GameObject _deathPrefab;
        [SerializeField] private GameObject _healthbarPrefab;
        [SerializeField] private Transform _healthbarOrigin;

        [Inject] private ContainerFactory Factory { get; set; }
        [Inject] private Shake Shake { get; set; }
        
        public RectTransform HUD { get; set; }
        
        private void Awake()
        {
            Lazy.VitalsPool.HealthIsOver += Die;
            Lazy.VitalsPool.TookDamage += PlayDamageEffect;
        }

        private void PlayDamageEffect(float _)
        {
            Factory.Instantiate<Transform>(_damagePrefab, Transform.position.WithY(1.5f));
        }

        private void Start()
        {
            Factory.Instantiate<Transform>(_deathPrefab, Transform.position.WithY(1.5f));
            FloatingHealthbar healthbar = Instantiate(_healthbarPrefab, HUD).GetComponent<FloatingHealthbar>();
            healthbar.Init(Lazy.VitalsPool.Health);
            healthbar.InitFloating(Camera.main, _healthbarOrigin, Lazy.VitalsPool);
        }

        private void Die()
        {
            Shake.AddImpulse(_deathShakeRatio);
            Destroy(gameObject);
        }
    }
}