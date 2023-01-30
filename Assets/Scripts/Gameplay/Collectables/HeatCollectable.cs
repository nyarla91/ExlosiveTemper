using System;
using Gameplay.Character.Player;
using Gameplay.Consumables;
using UnityEngine;

namespace Gameplay.Collectables
{
    public class HeatCollectable : ConsumableCollectable
    {
        protected override Func<PlayerComposition, bool> InstantUseCondition =>
            player => player.Resources.Heat.IsNotFull;

        protected override Func<PlayerComposition, Consumable> Consumable =>
            player => player.Inventory.HeatConsumable;
    }
}