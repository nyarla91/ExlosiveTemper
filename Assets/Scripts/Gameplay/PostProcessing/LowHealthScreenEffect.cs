using System;
using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Gameplay.PostProcessing
{
    public class LowHealthScreenEffect : LazyGetComponent<Volume>
    {
        [SerializeField] private float _scale;
        
        [Inject] private PlayerComposition Player { get; set; }

        private void Update()
        {
            Lazy.weight = (1 - Player.Vitals.Health.Percent) * _scale;
        }
    }
}