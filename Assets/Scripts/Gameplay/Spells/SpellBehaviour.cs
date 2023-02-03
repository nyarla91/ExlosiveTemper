using Content;
using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Spells
{
    public abstract class SpellBehaviour : Transformable
    {
        public Spell Spell { get; private set; }
        protected PlayerComposition Player { get; private set; }
        
        public void Init(Spell spell, PlayerComposition player)
        {
            Spell = spell;
            Player = player;
        }

        public abstract void OnCast();
    }
}