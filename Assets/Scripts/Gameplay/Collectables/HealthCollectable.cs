using System;
using Gameplay.Character.Player;
using Gameplay.Consumables;
using UnityEngine;

namespace Gameplay.Collectables
{
    public class HealthCollectable : ConsumableCollectable
    {
        protected override Func<PlayerComposition, bool> InstantUseCondition => player => player.Vitals.Health.IsNotFull;

        protected override Func<PlayerComposition, Consumable> Consumable =>
            player => player.Inventory.HealthConsumable;
    }
}