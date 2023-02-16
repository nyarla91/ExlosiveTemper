using System.Collections;
using Extentions;
using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public class RestorationSpell : SpellBehaviour
    {
        [SerializeField] private float _healthRestored;

        [Inject] private ContainerFactory Factory { get; set; }

        protected override void OnCast()
        {
            Player.Vitals.RestoreHealth(_healthRestored);
        }
    }
}