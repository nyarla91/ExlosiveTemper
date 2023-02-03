using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.VFX
{
    public class VisualEffect : MonoBehaviour
    {
        [SerializeField] private float _duration;

        private Timer _lifetime;

        public Timer Lifetime => _lifetime;
        protected virtual float DurationScale => 1;
        [Inject] private Pause Pause { get; set; }
        
        private void Start()
        {
            _lifetime = new Timer(this, _duration, Pause).Start();
            _lifetime.Expired += Dispose;
        }

        private void Dispose()
        {
            Destroy(gameObject);
        }
    }
}