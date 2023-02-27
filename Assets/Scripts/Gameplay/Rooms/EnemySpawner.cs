using System;
using System.Collections;
using System.Collections.Generic;
using Content;
using Extentions;
using Extentions.Factory;
using Gameplay.Character.Enemy;
using Gameplay.Character.Player;
using Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Rooms
{
    public class EnemySpawner : Transformable
    {
        [SerializeField] private PoolFactory _projectilesFactory;
        [SerializeField] private PoolFactory _healthBarsFactory;
        [SerializeField] private int _wavesPerRoom;
        [SerializeField] private int[] _waveTotalWeightInRoom;
        [SerializeField] private List<EnemySpawnDetails> _enemies;
        [SerializeField] private List<int> _numberOfElitesInRoom;
        [SerializeField] private int _bossLevelPerodicity;
        [SerializeField] private List<GameObject> _bossesInOrder;
        [SerializeField] private List<GameObject> _lateBossesPool;
        [SerializeField] private CollectableSpawnDetails[] _collectables;

        private readonly List<EnemyComposition> _enemiesAlive = new List<EnemyComposition>();
        
        [Inject] private ContainerFactory ContainerFactory { get; set; }
        [Inject] private PlayerComposition Player { get; set; }
        [Inject] private Pause Pause { get; set; }
        public RectTransform HUD { get; set; }

        public EnemyComposition[] EnemiesAlive => _enemiesAlive.ToArray();
        public bool IsCombatOn => _enemiesAlive.Count > 0;

        public event Action CombatIsOver;
        
        public void StartWave(Room room) => StartCoroutine(RoomCycle(room));

        private IEnumerator RoomCycle(Room room)
        {
            yield return new WaitForSeconds(0.1f);
            bool bossLevel = room.Level % _bossLevelPerodicity == 0;
            for (int wave = 0; wave < (bossLevel ? 1 : _wavesPerRoom); wave++)
            {
                SpawnWave(room, bossLevel);
                yield return new WaitUntil(() => _enemiesAlive.Count == 0);
                CombatIsOver?.Invoke();
                yield return new PausableWaitForSeconds(this, Pause, 1);
            }
        }

        private void SpawnWave(Room room, bool bossLevel)
        {
            int spentWeight = 0;
            if (bossLevel)
            {
                SpawnBoss(room);
            }
            else
            {
                SpawnRegularEnemiesWave(room, spentWeight);
            }
        }

        private void SpawnRegularEnemiesWave(Room room, int spentWeight)
        {
            int totalWeight = _waveTotalWeightInRoom.GetIndexOrLast(room.Level);
            for (int enemyI = 0; spentWeight < totalWeight; enemyI++)
            {
                EnemySpawnDetails spawnedEnemy = _enemies.PickRandomElement();
                GameObject prefab = enemyI < _numberOfElitesInRoom.GetIndexOrLast(room.Level)
                    ? spawnedEnemy.ElitePrefab
                    : spawnedEnemy.BasePrefab;

                EnemyComposition newEnemy = SpawnEnemy(room, prefab);
                GiveCollectableToEnemy(spawnedEnemy, newEnemy);

                spentWeight += spawnedEnemy.Weight;
            }
        }

        private void GiveCollectableToEnemy(EnemySpawnDetails spawnedEnemy, EnemyComposition newEnemy)
        {
            foreach (CollectableSpawnDetails collectable in _collectables)
            {
                if (collectable.DoesSpawnCollectableThisTime(spawnedEnemy.Weight))
                {
                    newEnemy.Status.InitDroppedItem(collectable.Prefab);
                }
            }
        }

        private void SpawnBoss(Room room)
        {
            int bossNumber = room.Level / _bossLevelPerodicity - 1;
            GameObject prefab = (bossNumber >= _bossesInOrder.Count)
                ? _lateBossesPool.PickRandomElement()
                : _bossesInOrder[bossNumber];
            SpawnEnemy(room, prefab);
        }

        private EnemyComposition SpawnEnemy(Room room, GameObject prefab)
        {
            Vector3 position = room.SpawnArea.bounds.RandomPointInBounds().WithY(0);
            EnemyComposition enemy = ContainerFactory.Instantiate<EnemyComposition>(prefab, position, Transform);
            
            _enemiesAlive.Add(enemy);
            enemy.Player = Player.Movement;
            enemy.AttackPattern.ProjectileFactory = _projectilesFactory;
            enemy.Vitals.HealthIsOver += () => _enemiesAlive.TryRemove(enemy);
            
            FloatingHealthbar healthbar = _healthBarsFactory.GetNewObject<FloatingHealthbar>(Vector3.zero, HUD);
            healthbar.ResourceBar.Init(enemy.Vitals.Health);
            healthbar.InitFloating(Camera.main, enemy.Status.HealthbarOrigin, enemy.Vitals);
            
            return enemy;
        }

        private void Awake()
        {
            Player.Sight.Spawner = this;
        }
    }
}