using Gameplay.Character.Player;

namespace Gameplay.Consumables
{
    public class HealthConsumable : Consumable
    {
        public override void ConsumeEffect(PlayerComposition player)
        {
            player.Vitals.RestoreHealth(50);
        }
    }
}