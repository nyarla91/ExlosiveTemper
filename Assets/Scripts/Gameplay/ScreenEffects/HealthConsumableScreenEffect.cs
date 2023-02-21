using Gameplay.Consumables;

namespace Gameplay.ScreenEffects
{
    public class HealthConsumableScreenEffect : ConsumableScreenEffect
    {
        protected override Consumable Consumable => Player.Inventory.HealthConsumable;
    }
}