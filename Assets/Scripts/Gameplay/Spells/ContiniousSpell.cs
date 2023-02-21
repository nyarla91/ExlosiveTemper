using System;
using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ContiniousSpell : SpellBehaviour
    {
        [SerializeField] private float _duration;

        private Timer _activeTimer;

        public bool IsActive => _activeTimer.IsOn;
        
        [Inject] private Pause Pause { get; set; }
        
        public event Action Activated;
        public event Action Finished;
        
        protected override void OnCast()
        {
            _activeTimer.Restart();
            Activated?.Invoke();
        }

        protected void Interrupt()
        {
            _activeTimer.Reset();
            OnFinish();
        }

        protected virtual void OnFinish()
        {
            Finished?.Invoke();
        }

        private void Awake()
        {
            _activeTimer = new Timer(this, _duration, Pause);
            _activeTimer.Expired += OnFinish;
        }
    }
}