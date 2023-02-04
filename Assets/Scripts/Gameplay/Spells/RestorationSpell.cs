using System.Collections;
using Extentions;
using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class RestorationSpell : SpellBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _healthRestored;

        [Inject] private ContainerFactory Factory { get; set; }
        
        public override void OnCast()
        {
            Player.Vitals.RestoreHealth(_healthRestored);
            Factory.Instantiate<Transform>(_effectPrefab, Transform.position.WithY(1.5f), Transform);
        }
    }
}