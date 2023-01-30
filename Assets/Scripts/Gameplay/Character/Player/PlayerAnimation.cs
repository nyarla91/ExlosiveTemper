﻿using Extentions;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerAnimation : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _spine;
        
        private void Update()
        {
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