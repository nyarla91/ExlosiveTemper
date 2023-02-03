using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.VFX
{
    public class ShotTrail : VisualEffect
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        private float _durationScale;

        protected override float DurationScale => _durationScale;

        public void Init(Vector3 origin, Vector3 target, float thickness, float duration)
        {
            Vector3[] points = {origin, target};
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(points);
            _lineRenderer.widthMultiplier = thickness;
            _durationScale = duration;
        }

        private void Update()
        {
            _lineRenderer.endColor = new Color(1, 1, 1, Lifetime.TimeLeft / Lifetime.Length);
        }
    }
}