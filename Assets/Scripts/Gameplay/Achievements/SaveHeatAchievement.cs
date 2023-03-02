using Gameplay.Character.Player;
using Gameplay.Rooms;
using Progression;
using UnityEngine;

namespace Gameplay.Achievements
{
    public class SaveHeatAchievement : AchievementBehaviour
    {
        [SerializeField] private float _heatRequired;

        private bool _startedWithHeat;
        
        public override void Init(Achievement achievement, PlayerComposition player, Room room, EnemySpawner enemySpawner)
        {
            base.Init(achievement, player, room, enemySpawner);
            Room.ComeToNextLevel += CheckStartingHeat;
            Spawner.CombatIsOver += CheckEndingHeat;
        }

        private void CheckEndingHeat()
        {
            if (_startedWithHeat && Player.Resources.Heat.Value >= _heatRequired)
                Complete();
            _startedWithHeat = false;
        }

        private void CheckStartingHeat(int obj)
        {
            if (Player.Resources.Heat.Value >= _heatRequired)
                _startedWithHeat = true;
        }

        private void OnDestroy()
        {
            Room.ComeToNextLevel -= CheckStartingHeat;
            Spawner.CombatIsOver -= CheckEndingHeat;
        }
    }
}