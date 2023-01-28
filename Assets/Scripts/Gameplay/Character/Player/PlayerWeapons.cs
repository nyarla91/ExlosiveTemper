using System;
using Extentions;
using Gameplay.Character.Player.Weapons;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerWeapons : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private Weapon _secondaryWeapon;
        [SerializeField] private float _chargedShotCooldownTime;

        private Timer _chargedShotCooldown;

        public Action<HitDetails> OnHit;

        private void Awake()
        {
            Lazy.Controls.OnShoot += TryShoot;
            Lazy.Controls.OnChargedShot += TryChargedShot;
            _chargedShotCooldown = new Timer(this, _chargedShotCooldownTime).Start();
        }

        private void TryShoot()
        {
            if (Lazy.StateMachine.IsCurrentStateNoneOf(StateMachine.Regular))
                return;
            _currentWeapon.TryShoot();
        }

        private void TryChargedShot()
        {
            if (Lazy.StateMachine.IsCurrentStateNoneOf(StateMachine.Regular) || _chargedShotCooldown.IsOn)
                return;
            if (_currentWeapon.TryChargedShot())
            {
                MiscExtentions.Swap(ref _currentWeapon, ref _secondaryWeapon);
                _chargedShotCooldown.Restart();
            }
        }
    }
}