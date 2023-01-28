using System.Collections;
using UnityEngine;

namespace Gameplay.Spells
{
    public class RestorationSpell : SpellBehaviour, IContiniousSpell
    {
        [SerializeField] private float _heathOverCast;
        [SerializeField] private float _heathAtTheEnd;
        
        public override void OnEndCast()
        {
            Player.Vitals.RestoreHealth(_heathAtTheEnd);
            StopAllCoroutines();
        }

        public void OnCastStart()
        {
            StartCoroutine(Restoration());
        }

        public void OnInterruptCast()
        {
            StopAllCoroutines();
        }

        private IEnumerator Restoration()
        {
            while (true)
            {
                Player.Vitals.RestoreHealth(_heathOverCast / Spell.CastTime * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}