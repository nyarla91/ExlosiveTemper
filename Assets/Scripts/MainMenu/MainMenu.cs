using Core;
using UnityEngine;
using Zenject;
using Menu = UIUtility.Menu;

namespace MainMenu
{
    public class MainMenu : Menu
    {
        [Inject] private SceneLoader SceneLoader {get; set; }
        
        public void Quit() => Application.Quit();
        public void StartGame() => SceneLoader.LoadGameplay();
    }
}