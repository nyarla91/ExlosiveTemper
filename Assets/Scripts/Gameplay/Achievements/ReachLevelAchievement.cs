using Gameplay.Character.Player;
using Gameplay.Rooms;
using Progression;
using UnityEngine;

namespace Gameplay.Achievements
{
    public class ReachLevelAchievement : AchievementBehaviour
    {
        [SerializeField] private int _levelRequired;

        public override void Init(Achievement achievement, PlayerComposition player, Room room, EnemySpawner enemySpawner)
        {
            base.Init(achievement, player, room, enemySpawner);
            Room.ComeToNextLevel += TryComplete;
        }

        private void TryComplete(int level)
        {
            if (_levelRequired == level)
            {
                Complete();
            }
        }

        private void OnDestroy()
        {
            Room.ComeToNextLevel -= TryComplete;
        }
    }
}