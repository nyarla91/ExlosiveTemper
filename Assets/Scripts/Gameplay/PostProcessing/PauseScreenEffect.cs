using Extentions;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using Zenject;

namespace Gameplay.PostProcessing
{
    public class PauseScreenEffect : LazyGetComponent<Volume>
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] [Range(0, 1)] private float _speed;

        private float _effectStrength;
        
        [Inject] private Pause Pause { get; set; }

        private void FixedUpdate()
        {
            float targetWeight = Pause.IsPaused ? 1 : 0;
            _effectStrength = Mathf.Lerp(_effectStrength, targetWeight, _speed);
            Lazy.weight = _effectStrength;
            _mixer.SetFloat("Dry", MathExtentions.EvaluateLine(0, -1000, _effectStrength));
            _mixer.SetFloat("Room", MathExtentions.EvaluateLine(-10000, 0, _effectStrength));
        }
    }
}