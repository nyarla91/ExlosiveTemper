using Extentions;
using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ForgingSpell : SpellBehaviour
    {
        [SerializeField] private int _embersGained;
        
        [Inject] private ContainerFactory Factory { get; set; }

        protected override void OnCast()
        {
            for (int i = 0; i < _embersGained; i++)
            {
                Player.Inventory.HeatConsumable.AddOne();
            }
        }
    }
}