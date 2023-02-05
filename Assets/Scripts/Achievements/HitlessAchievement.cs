using System;
using System.Linq;
using Gameplay.Character.Player;
using Gameplay.Rooms;
using UnityEngine;
using Zenject;

namespace Achievements
{
    public class HitlessAchievement : AchievementBehaviour
    {
        [SerializeField] private int[] _possibleLevels;

        private bool _isWatching;
        private bool _isDamaged;

        public override void Init(Achievement achievement, PlayerComposition player, Room room, EnemySpawner enemySpawner)
        {
            base.Init(achievement, player, room, enemySpawner);
            Room.ComeToNextLevel += StartWatching;
            Spawner.CombatIsOver += EndWatching;
            Player.Vitals.TookDamage += Damage;
        }

        private void Damage(float _)
        {
            _isDamaged = true;
        }

        private void StartWatching(int level)
        {
            if ( ! _possibleLevels.Contains(level))
                return;

            _isDamaged = false;
            _isWatching = true;
        }

        private void EndWatching()
        {
            if (_isWatching && ! _isDamaged)
                Complete();
            _isWatching = false;
        }

        private void OnDestroy()
        {
            Room.ComeToNextLevel -= StartWatching;
            Spawner.CombatIsOver -= EndWatching;
            Player.Vitals.TookDamage -= Damage;
        }
    }
}