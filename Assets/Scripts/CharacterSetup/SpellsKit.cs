using System;
using System.IO;
using System.Linq;
using Content;
using UnityEngine;

namespace CharacterSetup
{
    public class SpellsKit : MonoBehaviour
    {
        [SerializeField] private SpellsLibrary _library;
        [SerializeField] private Spell[] _eqipped;

        private string SavefilePath => Application.dataPath + "/setup.json";
        public Spell[] Eqipped => _eqipped;
        public bool HasEmptySlots => _eqipped.Any(spell => spell == null);

        public event Action<Spell[]> SpellsUpdated;

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

        public void Save()
        {
            int[] spellsToSave = new int[_eqipped.Length];
            for (int i = 0; i < _eqipped.Length; i++)
            {
                spellsToSave[i] = _library.GetSpellIndex(_eqipped[i]);
            }
            File.WriteAllText(SavefilePath, JsonUtility.ToJson(new SavableSpells(spellsToSave)));
        }

        private void Awake()
        {
            Load();
        }

        private void Load()
        {
            if ( ! File.Exists(SavefilePath))
                return;
            
            int[] spellsToEquip = JsonUtility.FromJson<SavableSpells>(File.ReadAllText(SavefilePath)).Spells;
            for (int i = 0; i < spellsToEquip.Length; i++)
            {
                _eqipped[i] = _library.GetSpell(spellsToEquip[i]);
            }
        }
    }

    [Serializable]
    public class SavableSpells
    {
        [SerializeField] private int[] _spells;

        public int[] Spells => _spells;
        
        public SavableSpells(int[] spells)
        {
            _spells = spells;
        }
    }
}