using System;
using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Rooms
{
    public class Room : Transformable
    {
        [SerializeField] private Door[] _doors;
        [SerializeField] private BoxCollider _spawnArea;
        
        public BoxCollider SpawnArea => _spawnArea;
        [Inject] public EnemySpawner Spawner { get; }
        
        public int Level { get; private set; }

        public event Action<int> ComeToNextLevel;
        
        public void NextLevel(Vector3 offset)
        {
            foreach (Door door in _doors)
            {
                door.Lock();
            }
            Transform.position += offset.WithY(0);
            Level++;
            ComeToNextLevel?.Invoke(Level);
            Spawner.StartWave(this);
        }
    }
}