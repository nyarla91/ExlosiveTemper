using System;
using System.Collections;
using System.Collections.Generic;
using Content;
using Extentions;
using Extentions.Factory;
using Gameplay.Character.Enemy;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Rooms
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _wavesPerRoom;
        [SerializeField] private int _waveTotalWeight;
        [SerializeField] private List<EnemySpawnDetails> _enemies;

        private readonly List<EnemyComposition> _enemiesAlive = new List<EnemyComposition>();
        
        [Inject] private ContainerFactory ContainerFactory { get; set; }
        [Inject] private PlayerComposition Player { get; set; }
        [Inject] private Pause Pause { get; set; }

        public EnemyComposition[] EnemiesAlive => _enemiesAlive.ToArray();
        public bool IsCombatOn => _enemiesAlive.Count > 0;
        
        public void StartWave(Room room) => StartCoroutine(Wave(room));

        private IEnumerator Wave(Room room)
        {
            yield return new WaitForSeconds(0.1f);
            for (int wave = 0; wave < _wavesPerRoom; wave++)
            {
                int spentWeight = 0;
                int enemiesLeft = 0;
                while (spentWeight < _waveTotalWeight)
                {
                    EnemySpawnDetails spawnedEnemy = _enemies.PickRandomElement();
                    Vector3 position = room.SpawnArea.bounds.RandomPointInBounds().WithY(0);
                    
                    EnemyComposition enemy = ContainerFactory.Instantiate<EnemyComposition>(spawnedEnemy.BasePrefab, position);
                    enemy.Player = Player;
                    _enemiesAlive.Add(enemy);
                    enemy.VitalsPool.OnHealthOver += () => _enemiesAlive.TryRemove(enemy);
                    
                    spentWeight += spawnedEnemy.Weight;
                }
                yield return new WaitUntil(() => enemiesLeft == 0);
                yield return new PausableWaitForSeconds(this, Pause, 1);
            }
        }

        private void Awake()
        {
            Player.Sight.Spawner = this;
        }
    }
}