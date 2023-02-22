using System;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SaveData
    {
        [SerializeField] private int[] _equippedSpells;
        [SerializeField] private List<int> _unlockedSpells;
        [SerializeField] private List<string> _seenTutorials;

        public int[] EquippedSpells
        {
            get => _equippedSpells;
            set => _equippedSpells = value;
        }

        public List<int> UnlockedSpells
        {
            get => _unlockedSpells;
            set => _unlockedSpells = value;
        }

        public List<string> SeenTutorials
        {
            get => _seenTutorials;
            set => _seenTutorials = value;
        }
    }
}