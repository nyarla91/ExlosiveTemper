using System;
using Extentions.Factory;
using UnityEngine;

namespace Gameplay.Rooms
{
    [Serializable]
    public class CollectableSpawnDetails
    {
        [field: SerializeField] public PoolFactory Factory { get; private set; }
        [field: SerializeField] public int WeightPereodicity { get; private set; }

        private int _weightTotal;
        
        public bool DoesSpawnCollectableThisTime(int weightAdiition)
        {
            _weightTotal += weightAdiition;
            if (_weightTotal < WeightPereodicity)
                return false;
            _weightTotal -= WeightPereodicity;
            return true;
        }
    }
}