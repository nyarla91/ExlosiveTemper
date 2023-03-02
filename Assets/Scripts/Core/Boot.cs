using UnityEngine;
using Zenject;

namespace Core
{
    public class Boot : MonoBehaviour
    {
        [Inject] private SceneLoader SceneLoader { get; set; }

        private void Start()
        {
            SceneLoader.LoadMainMenu();
        }
    }
}