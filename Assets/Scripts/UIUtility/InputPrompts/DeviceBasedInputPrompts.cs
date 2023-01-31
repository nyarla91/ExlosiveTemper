using System;
using Extentions;
using Input;
using TMPro;
using UnityEngine;
using Zenject;

namespace UIUtility.InputPrompts
{
    public abstract class DeviceBasedInputPrompts<TGraphics, TPrompt> : MonoBehaviour
    {
        [SerializeField] private TPrompt _keyboardMouse;
        [SerializeField] private TPrompt _xbox;
        [SerializeField] private TPrompt _playStation;

        protected TPrompt KeyboardMouse => _keyboardMouse;
        protected TPrompt Xbox => _xbox;
        protected TPrompt PlayStation => _playStation;
        
        private TGraphics _graphics;

        protected TGraphics Graphics => _graphics ??= GetComponent<TGraphics>();

        [Inject] private DeviceWatcher DeviceWatcher { get; set; }

        private void Awake()
        {
            DeviceWatcher.OnGamepadModelChanged += OnGamepadModelChanged;
            DeviceWatcher.OnInputSchemeChanged += OnInputSchemeChanged;
            UpdatePrompts();
        }

        private void OnGamepadModelChanged(GamepadModel _) => UpdatePrompts();
        private void OnInputSchemeChanged(InputScheme _) => UpdatePrompts();

        private void UpdatePrompts()
        {
            ApplyPrompt(DeviceWatcher.CurrentInputScheme switch
            {
                InputScheme.KeyboardMouse => _keyboardMouse,
                InputScheme.None => _keyboardMouse,
                InputScheme.Gamepad => DeviceWatcher.CurrentGamepadModel switch
                {
                    GamepadModel.PlayStation => _playStation,
                    GamepadModel.Xbox => _xbox,
                    GamepadModel.Switch => _xbox,
                    GamepadModel.None => _xbox,
                    _ => throw new ArgumentOutOfRangeException()
                },
                _ => throw new ArgumentOutOfRangeException()
            });
        }

        protected abstract void ApplyPrompt(TPrompt prompt);
    }
}