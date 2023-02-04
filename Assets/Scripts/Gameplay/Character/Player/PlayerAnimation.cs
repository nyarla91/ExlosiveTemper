using Extentions;
using Gameplay.VFX;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerAnimation : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private ParticleSystemEffect _sprintTrail;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _spine;
        
        [Inject] private Pause Pause { get; set; }
        
        private void Update()
        {
            _sprintTrail.Play = Lazy.Movement.IsSprinting;
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
        }

        private void LateUpdate()
        {
            _spine.rotation = Quaternion.LookRotation(Transform.forward, Vector3.up);
        }
    }
}