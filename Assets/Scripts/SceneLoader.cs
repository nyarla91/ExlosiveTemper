using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AssetReference _loadingReference;
    [SerializeField] private AssetReference _mainMenuReference;
    [SerializeField] private AssetReference _gameplayReference;
    
    public void LoadMainMenu() => LoadScene(_mainMenuReference);
    public void LoadGameplay() => LoadScene(_gameplayReference);

    private async void LoadScene(AssetReference scene)
    {
        await _loadingReference.LoadSceneAsync().Task;
        await scene.LoadSceneAsync().Task;
    }
}