using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ReloadSpell : SpellBehaviour
    {
        public override void OnEndCast()
        {
            Player.Weapons.EndChargedCooldown();
            Player.Weapons.SwapWeapons();
        }
    }
}