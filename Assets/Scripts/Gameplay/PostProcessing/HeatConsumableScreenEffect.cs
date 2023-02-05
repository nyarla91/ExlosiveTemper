using Gameplay.Consumables;
using UnityEngine;

namespace Gameplay.PostProcessing
{
    public class HeatConsumableScreenEffect : ConsumableScreenEffect
    {
        protected override Consumable Consumable => Player.Inventory.HeatConsumable;
    }
}