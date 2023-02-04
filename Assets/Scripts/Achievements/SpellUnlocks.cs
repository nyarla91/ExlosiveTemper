using System.Collections.Generic;
using System.IO;
using System.Linq;
using CharacterSetup;
using Content;
using Extentions;
using Extentions.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Achievements
{
    public class SpellUnlocks : Transformable
    {
        [SerializeField] private AchievementMessage _message;
        [SerializeField] private SpellsLibrary _library;
        [SerializeField] private List<Spell> _unlockedSpells;
        [SerializeField] private Achievement[] _achievements;

        public Achievement[] Achievements => _achievements;

        private string SavefilePath => Application.dataPath + "/permanent.json";

        public SavableSpells SavableSpells
        {
            get
            {
                int[] savedSpells = _unlockedSpells.Select(spell => _library.GetSpellIndex(spell)).ToArray();
                SavableSpells savableSpells = new SavableSpells(savedSpells);
                return savableSpells;
            }
        }

        public void UnlockSpell(Spell spell)
        {
            _unlockedSpells.Add(spell);
            _message.Show(spell.Achievement);
            Save();
        }

        private void Start()
        {
            Load();
        }

        private void Load()
        {
            if ( ! File.Exists(SavefilePath))
                return;

            _unlockedSpells = new List<Spell>();
            int[] spellsToEquip = JsonUtility.FromJson<SavableSpells>(File.ReadAllText(SavefilePath)).Spells;
            foreach (int spellIndex in spellsToEquip)
            {
                _unlockedSpells.Add(_library.GetSpell(spellIndex));
            }
        }

        private void Save()
        {
            var savableSpells = SavableSpells;
            File.WriteAllText(SavefilePath, JsonUtility.ToJson(savableSpells));
        }
    }
}