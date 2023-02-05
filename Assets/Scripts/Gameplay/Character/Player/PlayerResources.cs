using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerResources : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private Resource _heat;
        [SerializeField] private float _heatCollectPercent;
        
        public ResourceWrap Heat => _heat.Wrap;

        public bool TrySpendHeat(float value) => _heat.TrySpend(value);
        
        public void WasteHeat(float value) => _heat.Value -= value;

        public void AddHeat(float value)
        {
            if (value <= 0)
                return;
            _heat.Value += value;
        }
        
        private void Awake()
        {
            Lazy.Weapons.Hit += TryAddHeat;
            Lazy.Hitbox.TookHit += DiscardHeat;
        }

        private void DiscardHeat(HitDetails hitDetails)
        {
            if (hitDetails.Damage > 0)
                WasteHeat(100);
        }

        private void Start()
        {
            Lazy.Resources.AddHeat(50);
        }

        private void TryAddHeat(HitDetails hitDetails)
        {
            if (hitDetails.HitEntityOwner != EntityOwner.Enemy)
                return;
            _heat.Value += hitDetails.Damage * _heatCollectPercent;
        }
    }
}