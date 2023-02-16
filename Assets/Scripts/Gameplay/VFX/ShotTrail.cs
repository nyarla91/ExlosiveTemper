using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.VFX
{
    public class ShotTrail : TemporaryEffectInstance
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        public void Init(Vector3 origin, Vector3 target, float thickness)
        {
            Vector3[] points = {origin, target};
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(points);
            _lineRenderer.widthMultiplier = thickness;
        }

        private void Update()
        {
            _lineRenderer.endColor = new Color(1, 1, 1, Lifetime.TimeLeft / Lifetime.Length);
        }
    }
}