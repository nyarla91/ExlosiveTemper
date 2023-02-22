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
        [SerializeField] private Achievement[] _achievements;

        public Achievement[] Achievements => _achievements;

        [Inject] private Save.Save Save { get; set; }
        
        public bool IsSpellUnlocked(Spell spell) => Save.UnnlockedSpell.Contains(_library.GetSpellIndex(spell)); 
        
        public void UnlockSpell(Spell spell)
        {
            _message.Show(spell.Achievement);
            Save.AddUnlockedSpell(_library.GetSpellIndex(spell));
        }
    }
}