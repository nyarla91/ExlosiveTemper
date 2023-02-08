using System;
using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using Zenject;

namespace Gameplay.PostProcessing
{
    public class LowHealthScreenEffect : LazyGetComponent<Volume>
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AnimationCurve _curve;
        
        [Inject] private PlayerComposition Player { get; set; }

        private void Update()
        {
            float effectStrength = _curve.Evaluate(Player.Vitals.Health.Percent);
            Lazy.weight = effectStrength;
            _mixer.SetFloat("Pitch", 1 - effectStrength * 0.2f);
        }

        private void OnDestroy()
        {
            _mixer.SetFloat("Pitch", 1);
        }
    }
}