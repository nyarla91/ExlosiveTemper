using System.Linq;
using Extentions;
using UnityEngine;
using Zenject;

namespace Progression
{
    public class SpellUnlocks : Transformable
    {
        [SerializeField] private AchievementMessage _message;
        [SerializeField] private SpellsLibrary _library;
        [SerializeField] private Achievement[] _achievements;

        public Achievement[] Achievements => _achievements;

        [Inject] private Save.Save Save { get; set; }

        public void CompleteAchievement(Achievement achievement)
        {
            achievement.IsComplete = true;
            _message.Show(achievement);
            Save.AddCompleteAchievement(achievement.name);
        }

        private void Awake()
        {
            foreach (Achievement achievement in _achievements)
            {
                achievement.IsComplete = Save.CompleteAchievements.Contains(achievement.name);
            }
        }
    }
}