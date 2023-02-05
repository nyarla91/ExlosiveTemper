using Content;
using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public abstract class SpellBehaviour : Transformable
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _sound;
        
        public Spell Spell { get; private set; }
        protected PlayerComposition Player { get; private set; }
        
        public void Init(Spell spell, PlayerComposition player)
        {
            Spell = spell;
            Player = player;
        }

        public abstract void OnCast();

        protected void PlaySound() => _audioSource.PlayOneShot(_sound);
        protected void PlaySound(AudioClip clip) => _audioSource.PlayOneShot(clip);
    }
}