using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class VitalsPool : MonoBehaviour
    {
        [SerializeField] private Resource _health;
        [Tooltip("No damage can exceed this percent of max health")] [Range(0, 1)]
        [SerializeField] private float _maxPercentDamage = 1;

        public ResourceWrap Health => _health.Wrap;
        
        [Inject] private Pause Pause { get; set; }

        public bool IsDead { get; private set; }

        public void Init(int health, int shields, float shieldsRegeneration)
        {
            _health.Value = _health.MaxValue = health;
            IsDead = false;
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0 || IsDead)
                return;

            _health.Value -= Mathf.Min(damage, _health.MaxValue * _maxPercentDamage);
        }

        public void RestoreHealth(float health)
        {
            if (health <= 0 || IsDead)
                return;

            _health.Value += health;
        }

        public void Ressurect()
        {
            IsDead = false;
            _health.Value = _health.MaxValue;
        }

        private void Awake()
        {
            _health.OnOver += () => IsDead = true;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
                TakeDamage(10);
            if (UnityEngine.Input.GetKeyDown(KeyCode.G))
                RestoreHealth(10);
        }

        private void Start()
        {
            _health.Value = _health.MaxValue;
        }
    }
}