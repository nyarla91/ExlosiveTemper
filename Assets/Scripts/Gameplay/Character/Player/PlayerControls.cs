using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerControls : MonoBehaviour
    {
        private GameplayActions _actions;

        public Vector2 ThumbstickAim => _actions.Player.ThumbstickAim.ReadValue<Vector2>();
        public Vector2 MosueAim => _actions.Player.MouseAim.ReadValue<Vector2>();
        public Vector2 MoveVector => _actions.Player.Move.ReadValue<Vector2>();
        public bool IsSprintHolded { get; private set; }

        public event Action OnShoot;
        public event Action OnChargedShot;
        public event Action<int> OnSpellUse;
        
        private void Awake()
        {
            _actions = new GameplayActions();
            _actions.Enable();
            _actions.Player.Sprint.started += StartSprint;
            _actions.Player.Sprint.canceled += EndSprint;
            _actions.Player.Shoot.started += ShootInvoke;
            _actions.Player.ChargedShot.started += ChargedShotInvoke;
            _actions.Player.Spell1.canceled += FirstSpellUseInvoke;
            _actions.Player.Spell2.canceled += SecondSpellUseInvoke;
        }

        private void StartSprint(InputAction.CallbackContext _) => IsSprintHolded = true;
        private void EndSprint(InputAction.CallbackContext _) => IsSprintHolded = false;
        private void ShootInvoke(InputAction.CallbackContext _) => OnShoot?.Invoke();
        private void ChargedShotInvoke(InputAction.CallbackContext _) => OnChargedShot?.Invoke();
        private void FirstSpellUseInvoke(InputAction.CallbackContext _) => OnSpellUse?.Invoke(0);
        private void SecondSpellUseInvoke(InputAction.CallbackContext _) => OnSpellUse?.Invoke(1);

        private void OnDestroy()
        {
            _actions.Player.Sprint.started -= StartSprint;
            _actions.Player.Sprint.canceled -= EndSprint;
            _actions.Player.Shoot.started -= ShootInvoke;
            _actions.Player.ChargedShot.started -= ChargedShotInvoke;
        }
    }
}