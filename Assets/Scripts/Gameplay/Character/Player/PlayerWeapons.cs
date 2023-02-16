using System;
using Extentions;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerWeapons : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private Weapon _secondaryWeapon;
        [SerializeField] private float _chargedShotCooldownTime;

        private Timer _chargedShotCooldown;

        public Timer ChargedShotCooldown => _chargedShotCooldown;
        public Weapon CurrentWeapon => _currentWeapon;
        public Weapon SecondaryWeapon => _secondaryWeapon;
        
        [Inject] private Pause Pause { get; set; }

        public Action<HitDetails> Hit;
        public Action<Weapon> CurrentWeaponChanged;

        public void EndChargedCooldown() => _chargedShotCooldown.Reset();
        public void SwapWeapons()
        {
            MiscExtentions.Swap(ref _currentWeapon, ref _secondaryWeapon);
            _currentWeapon.gameObject.SetActive(true);
            _secondaryWeapon.gameObject.SetActive(false);
            CurrentWeaponChanged?.Invoke(_currentWeapon);
        }

        private void Awake()
        {
            Lazy.Controls.OnShoot += TryShoot;
            Lazy.Controls.OnChargedShot += TryChargedShot;
            _chargedShotCooldown = new Timer(this, _chargedShotCooldownTime, Pause);
        }

        private void Start()
        {
            CurrentWeaponChanged?.Invoke(_currentWeapon);
        }

        private void TryShoot()
        {
            if (Lazy.StateMachine.IsCurrentStateNoneOf(StateMachine.Regular) || Pause.IsPaused)
                return;
            _currentWeapon.TryShoot();
        }

        private void TryChargedShot()
        {
            if (Lazy.StateMachine.IsCurrentStateNoneOf(StateMachine.Regular) || Pause.IsPaused )
                return;
            if (_chargedShotCooldown.IsOn)
            {
                Lazy.View.PlayError();
                return;
            }
            if (!_currentWeapon.TryChargedShot())
                return;
            _chargedShotCooldown.Restart();
            SwapWeapons();
        }
    }
}