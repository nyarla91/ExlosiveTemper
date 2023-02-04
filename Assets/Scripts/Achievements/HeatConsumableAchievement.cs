using Gameplay.Consumables;
using UnityEngine;

namespace Achievements
{
    public class HeatConsumableAchievement : ConsumablesAchievement
    {
        protected override Consumable Consumable => Player.Inventory.HeatConsumable;
    }
}