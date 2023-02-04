using Gameplay.Consumables;
using UnityEngine;

namespace Achievements
{
    public class HealthConsumableAchievement : ConsumablesAchievement
    {
        protected override Consumable Consumable => Player.Inventory.HealthConsumable;
    }
}