using Extentions;
using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ForgingSpell : SpellBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private int _embersGained;
        
        [Inject] private ContainerFactory Factory { get; set; }
        
        public override void OnCast()
        {
            for (int i = 0; i < _embersGained; i++)
            {
                Player.Inventory.HeatConsumable.AddOne();
                Factory.Instantiate<Transform>(_effectPrefab, Transform.position.WithY(1.5f), Transform);
                PlaySound();
            }
        }
    }
}