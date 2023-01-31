using System;
using Extentions;
using Gameplay.Weapons;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerWeapons : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private Weapon _secondaryWeapon;
        [SerializeField] private float _chargedShotCooldownTime;

        private Timer _chargedShotCooldown;

        public Weapon CurrentWeapon => _currentWeapon;
        
        [Inject] private Pause Pause { get; set; }

        public Action<HitDetails> OnHit;

        public void EndChargedCooldown() => _chargedShotCooldown.Reset();
        public void SwapWeapons()
        {
            MiscExtentions.Swap(ref _currentWeapon, ref _secondaryWeapon);
        }

        private void Awake()
        {
            Lazy.Controls.OnShoot += TryShoot;
            Lazy.Controls.OnChargedShot += TryChargedShot;
            _chargedShotCooldown = new Timer(this, _chargedShotCooldownTime).Start();
        }

        private void TryShoot()
        {
            if (Lazy.StateMachine.IsCurrentStateNoneOf(StateMachine.Regular) || Pause.IsPaused)
                return;
            _currentWeapon.TryShoot();
        }

        private void TryChargedShot()
        {
            if (Lazy.StateMachine.IsCurrentStateNoneOf(StateMachine.Regular) || Pause.IsPaused || _chargedShotCooldown.IsOn)
                return;
            if (_currentWeapon.TryChargedShot())
            {
                _chargedShotCooldown.Restart();
                SwapWeapons();
            }
        }
    }
}