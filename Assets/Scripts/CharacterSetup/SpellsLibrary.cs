using System;
using System.Linq;
using Content;
using UnityEngine;

namespace CharacterSetup
{
    [CreateAssetMenu(menuName = "Spells Libary", order = 0)]
    public class SpellsLibrary : ScriptableObject
    {
        [SerializeField] private Spell[] _spells;

        public Spell GetSpell(int index) => _spells[index];
        public Spell[] IndexesToSpells(int[] indexes) => indexes.Select(GetSpell).ToArray();

        public int GetSpellIndex(Spell spell)
        {
            for (int i = 0; i < _spells.Length; i++)
            {
                if (_spells[i].Equals(spell))
                    return i;
            }
            throw new ArgumentOutOfRangeException($"Libary has no {spell} spell");
        }

        public int[] SpellsToIndexes(Spell[] spells) => spells.Select(GetSpellIndex).ToArray();
    }
}