using System;
using UnityEngine;

namespace Extentions
{
    [Serializable]
    public class Resource
    {
        [SerializeField] private float _maxValue;

        private float _value;

        public float Value
        {
            get => _value;
            set
            {
                value = Mathf.Clamp(value, 0, MaxValue);
                if (value.Equals(_value))
                    return;

                if (_value > 0 && value == 0)
                    OnOver?.Invoke();

                _value = value;
                OnChange?.Invoke(_value, MaxValue);
            }
        }

        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public bool IsFilled => Value.Equals(MaxValue);
        public bool IsUnfilled => ! IsFilled;

        private ResourceWrap _wrap;
        public ResourceWrap Wrap => _wrap ??= new ResourceWrap(this);

        public delegate void OnChangeHandler(float current, float max);
        public event OnChangeHandler OnChange;
        public event Action OnOver;
    }

    public class ResourceWrap
    {
        private readonly Resource _resource;

        public float Value => _resource.Value;
        public float MaxValue => _resource.MaxValue;

        public event Resource.OnChangeHandler OnChange;
        public event Action OnOver;

        public ResourceWrap(Resource resource)
        {
            _resource = resource;
            _resource.OnChange += (current, max) => OnChange?.Invoke(current, max);
            _resource.OnOver += () => OnOver?.Invoke();
        }
    }
}