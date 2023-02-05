using Extentions;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Gameplay.PostProcessing
{
    public class PauseScreenEffect : LazyGetComponent<Volume>
    {
        [SerializeField] [Range(0, 1)] private float _speed;

        [Inject] private Pause Pause { get; set; }

        private void FixedUpdate()
        {
            float targetWeight = Pause.IsPaused ? 1 : 0;
            Lazy.weight = Mathf.Lerp(Lazy.weight, targetWeight, _speed);
        }
    }
}