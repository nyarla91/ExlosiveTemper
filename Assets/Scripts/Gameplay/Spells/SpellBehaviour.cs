using System;
using Content;
using Extentions;
using Gameplay.Character.Player;

namespace Gameplay.Spells
{
    public abstract class SpellBehaviour : Transformable
    {
        public Spell Spell { get; private set; }
        protected PlayerComposition Player { get; private set; }

        public event Action Casted;
        
        public void Init(Spell spell, PlayerComposition player)
        {
            Spell = spell;
            Player = player;
        }

        public void Cast()
        {
            Casted?.Invoke();
            OnCast();
        }
        
        protected abstract void OnCast();
    }
}