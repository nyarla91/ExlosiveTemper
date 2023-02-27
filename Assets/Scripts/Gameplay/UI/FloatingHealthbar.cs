using Extentions.Factory;
using Gameplay.Character;
using UnityEngine;

namespace Gameplay.UI
{
    public class FloatingHealthbar : PooledObject
    {
        private Camera _mainCamera;
        private Transform _target;
        private VitalsPool _character;

        private ResourceBar _resourceBar;

        public ResourceBar ResourceBar => _resourceBar ??= GetComponent<ResourceBar>();

        public void InitFloating(Camera mainCamera, Transform target, VitalsPool character)
        {
            _mainCamera = mainCamera;
            _target = target;
            _character = character;
            _character.HealthIsOver += PoolDisable;
        }

        public override void PoolDisable()
        {
            base.PoolDisable();
            _character.HealthIsOver -= PoolDisable;
        }

        private void Update()
        {
            Vector2 screenPosition = _mainCamera.WorldToScreenPoint(_target.position);
            screenPosition = screenPosition * 1920 / Screen.width;
            RectTransform.anchoredPosition = screenPosition;
        }
    }
}