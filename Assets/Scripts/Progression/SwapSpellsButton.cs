using UnityEngine;
using Zenject;

namespace Progression
{
    public class SwapSpellsButton : MonoBehaviour
    {
        [Inject] private SpellsKit Kit { get; set; }

        public void Swap() => Kit.SwapSpell();
    }
}