using Gameplay.Character.Player;
using Gameplay.Weapons;
using UniSense;
using UnityEngine;
using Zenject;

namespace Gameplay.ImpactEffects
{
    public class AdaptiveTriggers : MonoBehaviour
    {
        private DualSenseGamepadState _state;
        
        [Inject] private Settings.Settings Settings { get; set; }
        [Inject] private PlayerWeapons Weapons { get; set; }

        private Weapon _currentWeapon;
        
        private void Awake()
        {
            Weapons.CurrentWeaponChanged += UpdateWeapon;
        }

        private void Update()
        {
            DualSenseGamepadHID dualSense = DualSenseGamepadHID.FindCurrent();
            if (dualSense == null)
                return;
            
            _state.RightTrigger = GetTriggerStateForForce(_currentWeapon.IsOnCooldown ? 0 : _currentWeapon.AdaptiveTriggersForce);
            _state.LeftTrigger = GetTriggerStateForForce(Weapons.ChargedShotCooldown.IsOn ? 0 : 0.3f);
            
            dualSense.SetGamepadState(_state);
        }

        private void UpdateWeapon(Weapon weapon) => _currentWeapon = weapon;

        private DualSenseTriggerState GetTriggerStateForForce(float force)
        {
            if (force.Equals(0))
                return new DualSenseTriggerState();
            return new DualSenseTriggerState
            {
                EffectType = DualSenseTriggerEffectType.SectionResistance,
                Section = new DualSenseSectionResistanceProperties()
                {
                    StartPosition = 0,
                    EndPosition = 25,
                    Force = (byte) (255 * force)
                }
            };
        }
    }
}