using Gameplay.Character.Player;
using Gameplay.Consumables;
using Zenject;

namespace Gameplay.ScreenEffects
{
    public abstract class ConsumableScreenEffect : ImpulseScreenEffect
    {
        [Inject] protected PlayerComposition Player { get; set; }
        
        protected abstract Consumable Consumable { get; }

        private void Awake()
        {
            Consumable.Consumed += CreateImpulse;
        }
    }
}