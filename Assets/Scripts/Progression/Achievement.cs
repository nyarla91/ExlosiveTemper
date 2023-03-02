using Content;
using Localization;
using UnityEngine;

namespace Progression
{
    [CreateAssetMenu(menuName = "Achievement")]
    public class Achievement : ScriptableObject
    {
        [field: SerializeField] public LocalizedString Description { get; private set; }
        [field: SerializeField] public Spell UnlockedSpell { get; set; }
        [field: SerializeField] public GameObject Behaviour { get; private set; }

        public bool IsComplete
        {
            get => UnlockedSpell.IsUnlocked;
            set
            {
                if (value)
                    UnlockedSpell.Unlock();
                else
                    UnlockedSpell.Lock(Description);
            }
        }
    }
}