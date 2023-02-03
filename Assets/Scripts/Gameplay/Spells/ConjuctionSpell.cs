using UnityEngine;

namespace Gameplay.Spells
{
    public class ConjuctionSpell : SpellBehaviour
    {
        [SerializeField] private int _embersGained;
        
        public override void OnCast()
        {
            for (int i = 0; i < _embersGained; i++)
            {
                Player.Inventory.HeatConsumable.AddOne();
            }
        }
    }
}