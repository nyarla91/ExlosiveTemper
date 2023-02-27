using System;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Tutorial
{
    public class PickUpTutorial : TutorialMenu
    {
        [Inject] private PlayerInventory Inventory { get; set; }

        private void Awake()
        {
            Inventory.HealthConsumable.QuantityChanged += _ => TryShowTutorial();
            Inventory.HeatConsumable.QuantityChanged += _ => TryShowTutorial();
        }
    }
}