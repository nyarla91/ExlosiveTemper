using Extentions;
using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class ReloadSpell : SpellBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        
        [Inject] private ContainerFactory Factory { get; set; }
        
        public override void OnCast()
        {
            Player.Weapons.EndChargedCooldown();
            Factory.Instantiate<Transform>(_effectPrefab, Transform.position.WithY(1.5f), Transform);
            PlaySound();
        }
    }
}