using System.Linq;
using CharacterSetup;
using Content;
using Localization;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Achievements
{
    [CreateAssetMenu(menuName = "Achievement")]
    public class Achievement : ScriptableObject
    {
        [field: SerializeField] private SpellsLibrary Library { get; set; }
        [field: SerializeField] public LocalizedString Description { get; private set; }
        [field: SerializeField] public Spell UnlockedSpell { get; set; }
        [field: SerializeField] public GameObject Behaviour { get; private set; }

        public bool IsAvailable(SavableSpells save)
        {
            bool isAvailable = ! save.Spells.Contains(Library.GetSpellIndex(UnlockedSpell));
            return isAvailable;
        }
    }
}