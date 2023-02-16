using System;
using CharacterSetup;
using Content;
using Extentions;
using Extentions.Factory;
using Gameplay.Spells;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerSpells : LazyGetComponent<PlayerComposition>
    {
        
        private SpellBehaviour[] _spellBehaviours;

        public SpellBehaviour[] SpellBehaviours => _spellBehaviours;

        [Inject] private Pause Pause { get; set; }
        [Inject] private SpellsKit Kit { get; set; }
        [Inject] private ContainerFactory ContainerFactory { get; set; }

        public event Action<int, SpellBehaviour> OnSpellLoaded;
        
        private void Awake()
        {
            Lazy.Controls.OnSpellUse += TryUseSpell;
        }

        private void TryUseSpell(int index)
        {
            SpellBehaviour spellToUse = _spellBehaviours[index];
            if (Pause.IsPaused)
            {
                return;
            }
            if (!Lazy.Resources.TrySpendHeat(spellToUse.Spell.HeatCost))
            {
                Lazy.View.PlayError();
                return;
            }
            spellToUse.Cast();
        }

        private void Start()
        {
            Spell[] spells = Kit.Eqipped;
            _spellBehaviours = new SpellBehaviour[spells.Length];
            for (var i = 0; i < spells.Length; i++)
            {
                LoadSpellBehaviour(i);
            }
        }

        private void LoadSpellBehaviour(int index)
        {
            Spell spell = Kit.Eqipped[index];
            GameObject prefab = spell.Behaviour;
            SpellBehaviour behaviour = ContainerFactory.Instantiate<SpellBehaviour>(prefab, Transform.position, Transform);
            behaviour.Init(spell, Lazy);
            _spellBehaviours[index] = behaviour;
            behaviour.Transform.localRotation = Quaternion.Euler(0, 0, 0);
            OnSpellLoaded?.Invoke(index, behaviour);
        }
    }
}