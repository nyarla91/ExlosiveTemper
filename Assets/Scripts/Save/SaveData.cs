using System;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SaveData
    {
        [SerializeField] private int[] _equippedSpells;
        [SerializeField] private List<string> _completeAchievements;
        [SerializeField] private List<string> _seenTutorials;

        public int[] EquippedSpells
        {
            get => _equippedSpells;
            set => _equippedSpells = value;
        }

        public List<string> CompleteAchievements
        {
            get => _completeAchievements;
            set => _completeAchievements = value;
        }

        public List<string> SeenTutorials
        {
            get => _seenTutorials;
            set => _seenTutorials = value;
        }
    }
}