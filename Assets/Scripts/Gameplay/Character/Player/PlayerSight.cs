using System;
using Extentions;
using Gameplay.Character.Enemy;
using Gameplay.Rooms;
using Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerSight : LazyGetComponent<PlayerComposition>
    {
        [Inject] private DeviceWatcher DeviceWatcher { get; set; }
        [Inject] private Settings.Settings Settings { get; set; }
        [Inject] private Pause Pause { get; set; }
        public EnemySpawner Spawner { get; set; }

        private void Start()
        {
            Lazy.CameraView.Init(Transform);
        }

        private void FixedUpdate()
        {
            if (Pause.IsPaused)
                return;
            if (Lazy.StateMachine.IsCurrentStateOneOf(StateMachine.Sprint))
                RotateTowardsMovement();
            else if (Lazy.StateMachine.IsCurrentStateOneOf(StateMachine.Regular))
                RotateTowardsAim();
        }

        private void RotateTowardsAim()
        {
            if (DeviceWatcher.CurrentInputScheme == InputScheme.Gamepad)
            {
                SetSightDirection(Lazy.CameraView.ScreenToPerspective(Lazy.Controls.MoveVector));
                SetSightDirection(Lazy.CameraView.ScreenToPerspective(Lazy.Controls.ThumbstickAim));
                return;
            }
            Ray ray = Lazy.CameraView.MainCamera.ScreenPointToRay(Lazy.Controls.MosueAim);
            Physics.Raycast(ray, out RaycastHit raycastHit, 1000, LayerMask.GetMask("MouseArea"), QueryTriggerInteraction.Collide);
            SetSightDirection((raycastHit.point - Transform.position).WithY(0).normalized);
        }

        private void RotateTowardsMovement()
        {
            SetSightDirection(Lazy.Movement.Velocity.normalized);
        }

        private void SetSightDirection(Vector3 direction)
        {
            direction = direction.WithY(0);
            if (direction.Equals(Vector3.zero))
                return;
            
            float maxAimAssistAngle = Settings.Config.Game.GetSettingValue("aim assist") * 3;
            if (maxAimAssistAngle > 0 && Spawner.EnemiesAlive != null)
            {
                float forwardDegreees = direction.XZtoXY().ToDegrees(); 
                Transform closestEnemy = null;
                float closestAngle = 360;
                foreach (EnemyComposition enemy in Spawner.EnemiesAlive)
                {
                    Vector3 enemyPosition = enemy.Transform.position;
                    float enemyDegrees = (enemyPosition - Transform.position).XZtoXY().ToDegrees();
                    float angle = Mathf.Abs(forwardDegreees - enemyDegrees);
                    if (angle < closestAngle)
                    {
                        closestEnemy = enemy.Transform;
                        closestAngle = angle;
                    }
                }
                if (closestEnemy != null && closestAngle < maxAimAssistAngle)
                {
                    direction = closestEnemy.position - Transform.position;
                }
            }
            
            direction.Normalize();
            Transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}