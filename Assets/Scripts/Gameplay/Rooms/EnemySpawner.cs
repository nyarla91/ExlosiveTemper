using System;
using System.Collections;
using System.Collections.Generic;
using Content;
using Extentions;
using Extentions.Factory;
using Gameplay.Character.Enemy;
using Gameplay.Character.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Gameplay.Rooms
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Room _room;
        [SerializeField] private int _wavesPerRoom;
        [SerializeField] private int _waveTotalWeight;
        [SerializeField] private List<EnemySpawnDetails> _enemies;
        [SerializeField] private List<int> _numberOfElitesInRoom;
        [SerializeField] private int _bossLevelPerodicity;
        [SerializeField] private List<GameObject> _bossesInOrder;
        [SerializeField] private List<GameObject> _lateBossesPool;

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
            bool bossLevel = room.Level % _bossLevelPerodicity == 0;
            for (int wave = 0; wave < (bossLevel ? 1 : _wavesPerRoom); wave++)
            {
                int spentWeight = 0;
                if (bossLevel)
                {
                    int bossNumber = room.Level / _bossLevelPerodicity - 1;
                    GameObject prefab = (bossNumber >= _bossesInOrder.Count)
                        ? _lateBossesPool.PickRandomElement()
                        : _bossesInOrder[bossNumber];
                    SpawnEnemy(room, prefab);
                }
                else
                {
                    for (int enemyI = 0; spentWeight < _waveTotalWeight; enemyI++)
                    {
                        EnemySpawnDetails spawnedEnemy = _enemies.PickRandomElement();
                        int roomLevel = _room.Level >= _numberOfElitesInRoom.Count ? _numberOfElitesInRoom.Count - 1 : _room.Level;
                        GameObject prefab = enemyI < _numberOfElitesInRoom[roomLevel] ? spawnedEnemy.ElitePrefab : spawnedEnemy.BasePrefab;

                        SpawnEnemy(room, prefab);
                        spentWeight += spawnedEnemy.Weight;
                    }
                }
                yield return new WaitUntil(() => _enemiesAlive.Count == 0);
                yield return new PausableWaitForSeconds(this, Pause, 1);
            }
        }

        private void SpawnEnemy(Room room, GameObject prefab)
        {
            Vector3 position = room.SpawnArea.bounds.RandomPointInBounds().WithY(0);
            EnemyComposition enemy = ContainerFactory.Instantiate<EnemyComposition>(prefab, position);
            enemy.Player = Player;
            _enemiesAlive.Add(enemy);
            enemy.VitalsPool.OnHealthOver += () => _enemiesAlive.TryRemove(enemy);
        }

        private void Awake()
        {
            Player.Sight.Spawner = this;
        }
    }
}