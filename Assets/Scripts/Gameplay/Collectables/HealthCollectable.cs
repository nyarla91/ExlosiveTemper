using System;
using Gameplay.Character.Player;
using Gameplay.Consumables;
using UnityEngine;

namespace Gameplay.Collectables
{
    public class HealthCollectable : ConsumableCollectable
    {
        protected override Func<PlayerComposition, bool> InstantUseCondition => _ => false;

        protected override Func<PlayerComposition, Consumable> Consumable =>
            player => player.Inventory.HealthConsumable;
    }
}