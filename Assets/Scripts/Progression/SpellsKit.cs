using System;
using System.Linq;
using Content;
using UnityEngine;
using Zenject;

namespace Progression
{
    public class SpellsKit : MonoBehaviour
    {
        [SerializeField] private SpellsLibrary _library;
        [SerializeField] private Spell[] _eqipped;

        public Spell[] Eqipped => _eqipped;
        public bool HasEmptySlots => _eqipped.Any(spell => spell == null);
        
        [Inject] private Save.Save Save { get; set; }

        public event Action<Spell[]> SpellsUpdated;

        public void SaveEquipped()
        {
            Save.UpdateEquippedSpells(_library.SpellsToIndexes(_eqipped));
        }

        public bool TryUnequipSpellInSlot(int slot)
        {
            if (_eqipped[slot] == null)
                return false;
            _eqipped[slot] = null;
            SpellsUpdated?.Invoke(_eqipped);
            return true;
        }

        public void SwapSpell()
        {
            Spell old0 = _eqipped[0];
            _eqipped[0] = _eqipped[1];
            _eqipped[1] = old0;
            SpellsUpdated?.Invoke(_eqipped);
        }

        public bool TryEquipSpell(Spell spell) => TryReplaceSlot(s => s == null, spell);

        public bool TryUnequipSpell(Spell spell) => TryReplaceSlot(s => s == spell, null);

        private bool TryReplaceSlot(Predicate<Spell> criteria, Spell newSpell)
        {
            for (int i = 0; i < _eqipped.Length; i++)
            {
                if ( ! criteria.Invoke(_eqipped[i]))
                    continue;

                _eqipped[i] = newSpell;
                SpellsUpdated?.Invoke(_eqipped);
                return true;
            }
            return false;
        }

        private void Start()
        {
            _eqipped = _library.IndexesToSpells(Save.EquippedSpells);
        }
    }
}