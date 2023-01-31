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

        public void MoveAndReset(Vector3 offset)
        {
            foreach (Door door in _doors)
            {
                door.Lock();
            }
            Transform.position += offset.WithY(0);
            Spawner.StartWave(this);
        }
    }
}