using Gameplay.Consumables;

namespace Gameplay.Achievements
{
    public class HealthConsumableAchievement : ConsumablesAchievement
    {
        protected override Consumable Consumable => Player.Inventory.HealthConsumable;
    }
}