using Gameplay.Character;
using UnityEngine;

namespace Gameplay.UI
{
    public class FloatingHealthbar : ResourceBar
    {
        private Camera _mainCamera;
        private Transform _target;

        public void InitFloating(Camera mainCamera, Transform target, VitalsPool character)
        {
            _mainCamera = mainCamera;
            _target = target;
            character.HealthIsOver += () => Destroy(gameObject);
        }

        private void Update()
        {
            Vector2 screenPosition = _mainCamera.WorldToScreenPoint(_target.position);
            RectTransform.anchoredPosition = screenPosition;
        }
    }
}