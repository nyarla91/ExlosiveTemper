using System;
using Extentions;
using UnityEngine;

namespace Gameplay
{
    public class CameraView : Transformable
    {
        [SerializeField] private Transform _followOrigin;
        [SerializeField] private Transform _roomCenter;
        [SerializeField] [Range(0, 1)] private float _playerInfluence;
        [SerializeField] private float _followSpeed;
        [SerializeField] private Camera _mainCamera;

        private Transform _followTarget;
        
        public Camera MainCamera => _mainCamera;
        
        public Vector3 ScreenToPerspective(Vector2 screen) => Transform.forward.WithY(0).normalized * screen.y + Transform.right * screen.x;

        public void Init(Transform followTarget)
        {
            _followTarget = followTarget;
        }

        private void Update()
        {
            Vector3 targetPosition = Vector3.Lerp(_roomCenter.position, _followTarget.position, _playerInfluence);
            _followOrigin.position = Vector3.Lerp(_followOrigin.position, targetPosition, Time.fixedDeltaTime * _followSpeed);
        }
    }
}