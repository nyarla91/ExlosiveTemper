using Gameplay.Character.Player;
using Zenject;

namespace Gameplay.ScreenEffects
{
    public class TakingDamageScreenEffect : ImpulseScreenEffect
    {
        [Inject] private PlayerComposition Player { get; set; }

        private void Awake()
        {
            Player.Vitals.TookDamage += damage => CreateImpulse(damage / Player.Vitals.Health.MaxValue);;
        }
    }
}