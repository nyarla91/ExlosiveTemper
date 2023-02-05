using System;
using DG.Tweening;
using Extentions;
using Gameplay.Character.Player;
using Gameplay.Rooms;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayMusic : LazyGetComponent<AudioSource>
    {
        [SerializeField] private float _fadeDuration;
        [SerializeField] [Range(0, 1)] private float _volumeScale;
        
        [Inject] private Room Room { get; set; }
        [Inject] private EnemySpawner EnemySpawner { get; set; }
        [Inject] private PlayerComposition Player { get; set; }

        private void Awake()
        {
            Room.ComeToNextLevel += Play;
            EnemySpawner.CombatIsOver += Stop;
            Player.Vitals.HealthIsOver += Stop;
        }

        private void Play(int i)
        {
            Lazy.DOKill();
            Lazy.DOFade(1 * _volumeScale, _fadeDuration);
        }

        private void Stop()
        {
            Lazy.DOKill();
            Lazy.DOFade(0, _fadeDuration);
            
        }
    }
}