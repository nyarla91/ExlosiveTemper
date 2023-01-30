using Gameplay.Character.Player;

namespace Gameplay.Consumables
{
    public class HeatConsumable : Consumable
    {
        public override void ConsumeEffect(PlayerComposition player)
        {
            player.Resources.AddHeat(50);
        }
    }
}