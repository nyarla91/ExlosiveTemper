using Gameplay;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayIntaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerOrigin;
        [SerializeField] private CameraView _cameraView;
        
        public override void InstallBindings()
        {
            Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
            GameObject player = Container.InstantiatePrefab(_playerPrefab, _playerOrigin);
            Container.Bind<PlayerComposition>().FromInstance(player.GetComponent<PlayerComposition>());
        }
    }
}