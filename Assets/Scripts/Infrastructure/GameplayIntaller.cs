using Gameplay;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayIntaller : MonoInstaller
    {
        [SerializeField] private CameraView _cameraView;
        
        public override void InstallBindings()
        {
            Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
        }
    }
}