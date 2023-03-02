using Gameplay.Character.Player;
using Gameplay.Rooms;
using Progression;

namespace Gameplay.Achievements
{
    public class FinishRunAchievement : AchievementBehaviour
    {
        public override void Init(Achievement achievement, PlayerComposition player, Room room, EnemySpawner enemySpawner)
        {
            base.Init(achievement, player, room, enemySpawner);
            Player.Vitals.HealthIsOver += Complete;
        }

        private void OnDestroy()
        {
            Player.Vitals.HealthIsOver -= Complete;
        }
    }
}