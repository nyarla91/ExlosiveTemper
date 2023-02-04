using System;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Tutorial
{
    public class PickUpTutorial : TutorialMenu
    {
        [Inject] private PlayerComposition Player { get; set; }

        private void Awake()
        {
            Player.Inventory.HealthConsumable.QuantityChanged += _ => TryShowTutorial();
            Player.Inventory.HeatConsumable.QuantityChanged += _ => TryShowTutorial();
        }
    }
}