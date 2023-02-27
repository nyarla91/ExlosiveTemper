using Achievements;
using CharacterSetup;
using Extentions;
using Input;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _sceneLoaderPrefab;
        [SerializeField] private GameObject _deviceWatcherPrefab;
        [SerializeField] private GameObject _settingsPrefab;
        [SerializeField] private GameObject _pausePrefab;
        [SerializeField] private GameObject _spellsKitPrefab;
        [SerializeField] private GameObject _spellUnlocksPrefab;
        [SerializeField] private GameObject _savePrefab;
        
        public override void InstallBindings()
        {
            BindFromPrefab<SceneLoader>(_sceneLoaderPrefab);
            BindFromPrefab<DeviceWatcher>(_deviceWatcherPrefab);
            BindFromPrefab<Save.Save>(_savePrefab);
            BindFromPrefab<Settings.Settings>(_settingsPrefab);
            BindFromPrefab<Pause>(_pausePrefab);
            BindFromPrefab<SpellsKit>(_spellsKitPrefab);
            BindFromPrefab<SpellUnlocks>(_spellUnlocksPrefab);
        }

        private void BindFromPrefab<T>(GameObject prefab)
        {
            GameObject instance = Container.InstantiatePrefab(prefab, transform);
            Container.Bind<T>().FromInstance(instance.GetComponent<T>()).AsSingle();
        }
    }
}