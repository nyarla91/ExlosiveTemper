using System;
using Extentions;
using Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyStatus : LazyGetComponent<EnemyComposition>
    {
        [SerializeField] private GameObject _healthbarPrefab;
        [SerializeField] private Transform _healthbarOrigin;

        public RectTransform HUD { get; set; }
        
        private void Awake()
        {
            Lazy.VitalsPool.HealthIsOver += Die;
        }

        private void Start()
        {
            FloatingHealthbar healthbar = Instantiate(_healthbarPrefab, HUD).GetComponent<FloatingHealthbar>();
            healthbar.Init(Lazy.VitalsPool.Health);
            healthbar.InitFloating(Camera.main, _healthbarOrigin, Lazy.VitalsPool);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}