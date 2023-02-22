using Extentions;
using Gameplay.Character.Player;
using Zenject;

namespace Gameplay.Spells.View
{
    public class ChargeOfAshesSpellExplosionView : LazyGetComponent<ChargeOfAshesSpell>
    {
        [Inject] private PlayerWeapons Weapons { get; set; }
        
        private void Awake()
        {
            
        }
    }
}