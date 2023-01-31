using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
        public event Action OnConsumeHealth;
        public event Action OnConsumeHeat;
        public event Action OnInteract;
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
            _actions.Player.ConsumeHealth.started += ConsumeHealthInvoke;
            _actions.Player.ConsumeHeat.started += ConsumeHeatInvoke;
            _actions.Player.Interact.performed += InteractInvoke;
        }

        private void StartSprint(InputAction.CallbackContext _) => IsSprintHolded = true;
        private void EndSprint(InputAction.CallbackContext _) => IsSprintHolded = false;
        private void ShootInvoke(InputAction.CallbackContext _) => OnShoot?.Invoke();
        private void ChargedShotInvoke(InputAction.CallbackContext _) => OnChargedShot?.Invoke();
        private void ConsumeHealthInvoke(InputAction.CallbackContext _) => OnConsumeHealth?.Invoke();
        private void ConsumeHeatInvoke(InputAction.CallbackContext _) => OnConsumeHeat?.Invoke();
        private void FirstSpellUseInvoke(InputAction.CallbackContext _) => OnSpellUse?.Invoke(0);
        private void SecondSpellUseInvoke(InputAction.CallbackContext _) => OnSpellUse?.Invoke(1);
        private void InteractInvoke(InputAction.CallbackContext _) => OnInteract?.Invoke();

        private void OnDestroy()
        {
            _actions.Player.Sprint.started -= StartSprint;
            _actions.Player.Sprint.canceled -= EndSprint;
            _actions.Player.Shoot.started -= ShootInvoke;
            _actions.Player.ChargedShot.started -= ChargedShotInvoke;
            _actions.Player.Spell1.canceled -= FirstSpellUseInvoke;
            _actions.Player.Spell2.canceled -= SecondSpellUseInvoke;
            _actions.Player.ConsumeHealth.started -= ConsumeHealthInvoke;
            _actions.Player.ConsumeHeat.started -= ConsumeHeatInvoke;
        }
    }
}