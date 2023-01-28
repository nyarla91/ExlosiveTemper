using System;
using Extentions;
using Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerSight : LazyGetComponent<PlayerComposition>
    {
        [Inject] private DeviceWatcher DeviceWatcher { get; set; }

        private void FixedUpdate()
        {
            if (Lazy.Movement.IsSprinting)
                RotateTowardsMovement();
            else
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
            if (!Physics.Raycast(ray, out RaycastHit raycastHit, 1000, LayerMask.GetMask("MouseArea")))
                return;
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
            direction.Normalize();
            Transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}