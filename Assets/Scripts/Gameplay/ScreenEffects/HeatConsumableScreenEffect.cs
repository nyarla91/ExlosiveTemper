using Gameplay.Consumables;

namespace Gameplay.ScreenEffects
{
    public class HeatConsumableScreenEffect : ConsumableScreenEffect
    {
        protected override Consumable Consumable => Player.Inventory.HeatConsumable;
    }
}