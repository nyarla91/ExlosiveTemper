using Gameplay.Character.Player;
using Gameplay.Rooms;
using Progression;
using UnityEngine;
using Zenject;

namespace Gameplay.Achievements
{
    public class AchievementBehaviour : MonoBehaviour
    {
        protected EnemySpawner Spawner { get; private set; }
        protected PlayerComposition Player { get; private set; }
        protected Room Room { get; private set; }
        protected Achievement Achievement { get; private set; }
        [Inject] private SpellUnlocks Unlocks { get; set; }

        public virtual void Init(Achievement achievement, PlayerComposition player, Room room, EnemySpawner enemySpawner)
        {
            Achievement = achievement;
            Player = player;
            Room = room;
            Spawner = enemySpawner;
        }

        protected void Complete()
        {
            Unlocks.CompleteAchievement(Achievement);
            Destroy(gameObject);
        }
    }
}