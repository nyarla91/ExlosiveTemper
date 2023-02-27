using Gameplay;
using Gameplay.Character.Player;
using Gameplay.ImpactEffects;
using Gameplay.Rooms;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayIntaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerOrigin;
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Room _room;
        [SerializeField] private Shake _shake;
        
        public override void InstallBindings()
        {
            Container.Bind<Room>().FromInstance(_room);
            Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
            Container.Bind<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
            GameObject player = Container.InstantiatePrefab(_playerPrefab, _playerOrigin);
            PlayerComposition composition = player.GetComponent<PlayerComposition>();
            Container.Bind<PlayerComposition>().FromInstance(composition);
            Container.Bind<PlayerMovement>().FromInstance(composition.Movement);
            Container.Bind<PlayerWeapons>().FromInstance(composition.Weapons);
            Container.Bind<PlayerInventory>().FromInstance(composition.Inventory);
            Container.Bind<Shake>().FromInstance(_shake);
        }
    }
}