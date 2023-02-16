using System;
using Extentions;
using Gameplay.Collectables;
using Gameplay.Consumables;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerInventory : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _pickUpSound;
        public HealthConsumable HealthConsumable { get; } = new HealthConsumable();
        public HeatConsumable HeatConsumable { get; } = new HeatConsumable();

        private void Awake()
        {
            Lazy.Controls.OnConsumeHealth += TryConsumeHealth;
            Lazy.Controls.OnConsumeHeat += TryConsumeHeat;
        }

        private void TryConsumeHealth()
        {
            if ( ! HealthConsumable.TryConsume(Lazy))
                Lazy.View.PlayError();
        }

        private void TryConsumeHeat()
        {
            if (!HeatConsumable.TryConsume(Lazy))
                Lazy.View.PlayError();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectable collectable))
            {
                collectable.OnCollect(Lazy);
                collectable.PoolDisable();
                _audioSource.PlayOneShot(_pickUpSound);
            }
        }
    }
}