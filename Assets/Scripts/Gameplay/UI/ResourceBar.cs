using System;
using Extentions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class ResourceBar : Transformable
    {
        [SerializeField] private ResourcesShowType _valueShowType;
        [SerializeField] private RectMask2D _mask;
        [SerializeField] private RectMask2D _increaseMask;
        [SerializeField] private RectMask2D _decreaseMask;
        [SerializeField] private TMP_Text _counter;
        [SerializeField] private int _side;
        [SerializeField] private float _sharpness = 10;

        private ResourceWrap _resource;

        private float _actualPercent;
        private float _impactPercent;
        
        public void Init(ResourceWrap resource)
        {
            _resource = resource;
            _resource.Changed += ApplyValue;
            ApplyValue(resource.Value, resource.MaxValue);
        }

        private void ApplyValue(float current, float max)
        {
            if (_counter != null)
            {
                _counter.text = _valueShowType switch
                {
                    ResourcesShowType.None => "",
                    ResourcesShowType.OnlyCurrent => $"{Mathf.RoundToInt(current)}",
                    ResourcesShowType.CurrentAndMax => $"{Mathf.RoundToInt(current)}/{Mathf.RoundToInt(max)}",
                    ResourcesShowType.Percent => $"{Mathf.RoundToInt(current / max * 100)}%",
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            _actualPercent = (current / max);
        }

        private void FixedUpdate()
        {
            _impactPercent = Mathf.Lerp(_impactPercent, _actualPercent, Time.fixedDeltaTime * _sharpness);
            bool decreasing = _actualPercent < _impactPercent;
            RectMask2D hidenMask = decreasing ? _increaseMask : _decreaseMask;
            RectMask2D actualMask = decreasing ? _mask : _increaseMask;
            RectMask2D impactMask = decreasing ? _decreaseMask : _mask;
            
            SetMaskFillPercent(hidenMask, 0);
            SetMaskFillPercent(actualMask, _actualPercent);
            SetMaskFillPercent(impactMask, _impactPercent);
        }

        private void SetMaskFillPercent(RectMask2D mask, float percent)
        {
            percent = Mathf.Clamp(percent, 0, 1);
            
            Rect rect = RectTransform.rect;
            float sidePadding = (_side.IsEven() ? rect.width : rect.height) * (1 - percent);
            mask.padding = new Vector4(GetSidePadding(0), GetSidePadding(1), GetSidePadding(2), GetSidePadding(3));

            float GetSidePadding(int side)
            {
                return _side == side ? sidePadding : 0;
            }
        }

        private void OnDestroy()
        {
            if (_resource != null)
            {
                _resource.Changed -= ApplyValue;
            }
        }

        private enum ResourcesShowType
        {
            None,
            OnlyCurrent,
            CurrentAndMax,
            Percent
        }
    }
}