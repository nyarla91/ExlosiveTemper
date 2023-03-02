using Extentions;
using Extentions.Factory;
using Gameplay.Character.Player;
using Gameplay.Rooms;
using Progression;
using UnityEngine;
using Zenject;

namespace Gameplay.Achievements
{
    public class AchievementActivator : Transformable
    {
        [Inject] private ContainerFactory Factory { get; set; }
        [Inject] private SpellUnlocks Unlocks { get; set; }
        [Inject] private PlayerComposition Player { get; set; }
        [Inject] private Room Room { get; set; }
        [Inject] private EnemySpawner EnemySpawner { get; set; }

        private void Start()
        {
            InstantiateAchievements();
        }

        public void InstantiateAchievements()
        {
            foreach (Achievement achievement in Unlocks.Achievements)
            {
                if ( ! achievement.IsComplete)
                    InstantiateAchievement(achievement);
            }

            void InstantiateAchievement(Achievement achievement)
            {
                GameObject prefab = achievement.Behaviour;
                AchievementBehaviour behaviour =
                    Factory.Instantiate<AchievementBehaviour>(prefab, Vector3.zero, Transform);
                behaviour.Init(achievement, Player, Room, EnemySpawner);
                behaviour.gameObject.SetActive(true);
            }
        }
    }
}