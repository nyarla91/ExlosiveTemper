using Gameplay.Consumables;

namespace Gameplay.PostProcessing
{
    public class HealthConsumableScreenEffect : ConsumableScreenEffect
    {
        protected override Consumable Consumable => Player.Inventory.HealthConsumable;
    }
}