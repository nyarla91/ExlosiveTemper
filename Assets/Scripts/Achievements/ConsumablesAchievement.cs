using System;
using Gameplay.Character.Player;
using Gameplay.Consumables;
using Gameplay.Rooms;
using UnityEngine;
using Zenject;

namespace Achievements
{
    public abstract class ConsumablesAchievement : AchievementBehaviour
    {
        [SerializeField] private int _quantityRequired;
        
        protected abstract Consumable Consumable { get; }

        public override void Init(Achievement achievement, PlayerComposition player, Room room, EnemySpawner enemySpawner)
        {
            base.Init(achievement, player, room, enemySpawner);
            Consumable.QuantityChanged += CheckQuantity;
        }

        private void CheckQuantity(int quantity)
        {
            if (quantity >= _quantityRequired)
                Complete();
        }
    }
}