using System;
using Extentions;
using Gameplay.Character;
using Gameplay.Character.Player;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Gameplay.PostProcessing
{
    public class TakingDamageScreenEffect : ImpulseScreenEffect
    {
        [Inject] private PlayerComposition Player { get; set; }

        private void Awake()
        {
            Player.Vitals.TookDamage += damage => CreateImpulse(damage / Player.Vitals.Health.MaxValue);;
        }
    }
}