using System;
using Gameplay.Character.Player;
using Gameplay.Consumables;
using UnityEngine;

namespace Gameplay.Collectables
{
    public abstract class ConsumableCollectable : Collectable
    {
        protected abstract Func<PlayerComposition, bool> InstantUseCondition { get; }
        protected abstract Func<PlayerComposition, Consumable> Consumable { get; }
        
        public override void OnCollect(PlayerComposition player)
        {
            if (InstantUseCondition.Invoke(player))
            {
                Consumable.Invoke(player).ConsumeEffect(player);
                return;
            }
            Consumable.Invoke(player).AddOne();
        }
    }
}