using System;
using System.Collections;
using Extentions;
using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.VFX
{
    public class TemporaryEffectInstance : PooledObject
    {
        [SerializeField] private float _disableTime;

        private Timer _lifetime;

        public Timer Lifetime => _lifetime;
        [Inject] protected Pause Pause { get; private set; }

        public override void OnPoolEnabled()
        {
            base.OnPoolEnabled();
            _lifetime.Restart();
        }

        protected virtual void Awake()
        {
            _lifetime = new Timer(this, _disableTime, Pause);
            _lifetime.Expired += PoolDisable;
        }
    }
}