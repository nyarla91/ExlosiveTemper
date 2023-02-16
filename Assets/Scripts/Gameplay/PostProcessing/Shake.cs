using Extentions;
using UniSense;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.PostProcessing
{
    public class Shake : Transformable
    {
        [SerializeField] [Range(0, 1)] private float _fadeOutSpeed;
        [SerializeField] private float _screenScale = 1;
        [SerializeField] private float _rumbleScale = 1;
        
        private float _force;

        [Inject] private Settings.Settings Settings { get; set; }

        private bool IsScreenShakeEnabled => Settings.Config.Game.IsSettingToggled("screen shake");
        private bool IsGamepadRumbleEnabled => Settings.Config.Game.IsSettingToggled("gamepad rumble");
        
        public void AddImpulse(float impulse)
        {
            if (impulse <= 0)
                return;
            _force += impulse;
        }

        private void FixedUpdate()
        {
            _force *= (1 - _fadeOutSpeed);
            
            Vector3 screenShake = IsScreenShakeEnabled ? (Random.insideUnitCircle * _force * _screenScale) : Vector3.zero;
            Transform.localPosition = screenShake;

            float gamepadRumble = IsGamepadRumbleEnabled ? (_force * _rumbleScale) : 0;
            Gamepad.current?.SetMotorSpeeds(gamepadRumble, 0);
            DualSenseGamepadHID.current?.SetMotorSpeeds(gamepadRumble, 0);
            
        }
    }
}