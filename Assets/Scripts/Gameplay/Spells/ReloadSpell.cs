using Extentions;
using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ReloadSpell : SpellBehaviour
    {
        [Inject] private ContainerFactory Factory { get; set; }

        protected override void OnCast()
        {
            Player.Weapons.EndChargedCooldown();
        }
    }
}