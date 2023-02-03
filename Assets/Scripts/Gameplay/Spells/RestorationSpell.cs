using System.Collections;
using UnityEngine;

namespace Gameplay.Spells
{
    public class RestorationSpell : SpellBehaviour
    {
        [SerializeField] private float _healthRestored;
        
        public override void OnCast()
        {
            Player.Vitals.RestoreHealth(_healthRestored);
        }
    }
}