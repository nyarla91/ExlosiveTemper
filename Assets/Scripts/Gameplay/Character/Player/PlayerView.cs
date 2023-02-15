using Extentions;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerView : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] [Range(0, 1)] private float _sprintSoundLerpSpeed;
        [SerializeField] private VisualEffect _sprintTrail;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _spine;
        [SerializeField] private AudioSource _errorAudioSource;

        [Inject] private Pause Pause { get; set; }

        public void PlayError() => _errorAudioSource.Play();
        
        private void Update()
        {
            UpdateSprintTrail();

            if (Pause.IsPaused)
            {
                _animator.speed = 0;
                return;
            }

            _animator.speed = 1;
            
            int runState;
            if (Lazy.Movement.Velocity.Equals(Vector3.zero))
                runState = 0;
            else
            {
                float moveDegree = Lazy.Movement.Velocity.normalized.XZtoXY().ToDegrees();
                float aimDegree = Transform.forward.XZtoXY().ToDegrees();
                float differance = Mathf.Repeat(aimDegree - moveDegree, 360);
            
                runState = Mathf.RoundToInt(differance / 90) + 1;
                if (runState == 5)
                    runState = 1;
            }
            _animator.SetInteger("Weapon", Lazy.Weapons.CurrentWeapon.AnimationIndex);
            _animator.SetInteger("RunState", runState);
            _animator.SetFloat("RunSpeed", Lazy.Movement.Velocity.magnitude);

            float targetSprintVolume = Lazy.Movement.IsSprinting ? 1 : 0;
        }

        private void UpdateSprintTrail()
        {
            _sprintTrail.pause = Pause.IsPaused;
            if (Lazy.Movement.IsSprinting)
                _sprintTrail.Play();
            else
                _sprintTrail.Stop();
        }

        private void LateUpdate()
        {
            _spine.rotation = Quaternion.LookRotation(Transform.forward, Vector3.up);
        }
    }
}