using UnityEngine;
using Zenject;

namespace CharacterSetup
{
    public class SwapSpellsButton : MonoBehaviour
    {
        [Inject] private SpellsKit Kit { get; set; }

        public void Swap() => Kit.SwapSpell();
    }
}