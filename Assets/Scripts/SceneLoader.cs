using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AssetReference _loadingReference;
    [SerializeField] private AssetReference _mainMenuReference;
    [SerializeField] private AssetReference _gameplayReference;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private RectTransform _blackScreen;

    private bool _loadingScene;
    
    public void LoadMainMenu() => LoadScene(_mainMenuReference);
    public void LoadGameplay() => LoadScene(_gameplayReference);

    private async void LoadScene(AssetReference scene)
    {
        if (_loadingScene)
            return;
        _loadingScene = true;
        
        _blackScreen.localRotation = Quaternion.Euler(0, 0, 90);
        _blackScreen.DOLocalRotate(new Vector3(0, 0, 0), _transitionDuration);
        await Task.Delay((int) (_transitionDuration * 1000));
        
        await _loadingReference.LoadSceneAsync().Task;
        await scene.LoadSceneAsync().Task;
        
        _blackScreen.DOLocalRotate(new Vector3(0, 0, -90), _transitionDuration);
        await Task.Delay((int) (_transitionDuration * 1000));
        
        _loadingScene = false;
    }
}