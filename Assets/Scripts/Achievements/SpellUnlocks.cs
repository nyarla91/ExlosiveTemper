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

        private List<AchievementBehaviour> _achievementBehaviours = new List<AchievementBehaviour>();
        private string SavefilePath => Application.dataPath + "/permanent.json";

        [Inject] private ContainerFactory Factory { get; set; }

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

        public void InstantiateAchievements()
        {
            foreach (Achievement achievement in _achievements)
            {
                if (achievement.IsAvailable(SavableSpells))
                    InstantiateAchievement(achievement);
            }

            async void InstantiateAchievement(Achievement achievement)
            {
                AssetReference reference = achievement.BehaviourReference;
                GameObject prefab = await reference.LoadAssetAsync<GameObject>().Task;
                AchievementBehaviour behaviour = Factory.Instantiate<AchievementBehaviour>(prefab, Transform.position, Transform);
                behaviour.Init(achievement);
                _achievementBehaviours.Add(behaviour);
            }
        }

        public void DisposeAchievements()
        {
            foreach (var behaviour in _achievementBehaviours)
            {
                behaviour.Achievement.BehaviourReference.ReleaseAsset();
                Destroy(behaviour.gameObject);
            }
            _achievementBehaviours = new List<AchievementBehaviour>();
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