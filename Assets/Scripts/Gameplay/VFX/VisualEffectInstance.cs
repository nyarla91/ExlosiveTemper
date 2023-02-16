using System.Collections;
using Extentions;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Gameplay.VFX
{
    public class VisualEffectInstance : TemporaryEffectInstance
    {
        [SerializeField] private VisualEffect _vfx;
        [SerializeField] private float _emitDuration;

        private Timer _emissionSpan;
        private bool _emit;
            
        public bool Emit
        {
            get => _emit;
            set
            {
                _emit = value;
                _vfx.playRate = value ? 1 : 0;
            }
        }

        public override void OnPoolEnable()
        {
            base.OnPoolEnable();
            _vfx.Play();
            _emissionSpan?.Restart();
            Emit = true;
        }

        public override void PoolDisable()
        {
            _vfx.Stop();
            base.PoolDisable();
        }

        protected override void Awake()
        {
            base.Awake();
            if (_emitDuration > 0)
            {
                _emissionSpan = new Timer(this, _emitDuration, Pause);
                _emissionSpan.Expired += () => Emit = false;
            }
        }

        private void Update()
        {
            _vfx.pause = Pause.IsPaused;
        }
    }
}