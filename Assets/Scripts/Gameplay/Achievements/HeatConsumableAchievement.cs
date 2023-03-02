using Gameplay.Consumables;

namespace Gameplay.Achievements
{
    public class HeatConsumableAchievement : ConsumablesAchievement
    {
        protected override Consumable Consumable => Player.Inventory.HeatConsumable;
    }
}