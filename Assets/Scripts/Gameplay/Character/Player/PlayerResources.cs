using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerResources : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private Resource _heat;
        
        public ResourceWrap Heat => _heat.Wrap;

        public void WasteHeat(float value) => _heat.Value -= value;

        private void Awake()
        {
            Lazy.Weapons.OnHit += TryAddHeat;
        }

        private void TryAddHeat(HitDetails hitDetails)
        {
            if (hitDetails.HitEntityOwner != EntityOwner.Enemy)
                return;
            _heat.Value += 25;
        }
    }
}