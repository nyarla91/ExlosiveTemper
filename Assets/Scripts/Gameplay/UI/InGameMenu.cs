using System;
using System.Threading.Tasks;
using Extentions;
using Input;
using UIUtility;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.UI
{
    public class InGameMenu : Menu
    {
        [Inject] private SceneLoader SceneLoader { get; set; }
        [Inject] private Pause Pause { get; set; }

        public void Quit() => Application.Quit();
        public void Surrender() => SceneLoader.LoadMainMenu();

        protected override void Awake()
        {
            base.Awake();
            OnOpen += UnsubscribeMenuOpen;
            OnClose += SubscribeMenuOpen;
        }
        
        private async void SubscribeMenuOpen()
        {
            await Task.Delay(1);
            MenuControls.Actions.Always.OpenMenu.started += OpenMenuIfClosed;
        }

        private void UnsubscribeMenuOpen() => MenuControls.Actions.Always.OpenMenu.started -= OpenMenuIfClosed;

        private async void OpenMenuIfClosed(InputAction.CallbackContext _)
        {
            if (Pause.IsPaused)
                return;
            Open();
        }

        private void OnDestroy()
        {
            if ( ! IsOpened)
                UnsubscribeMenuOpen();
        }
    }
}