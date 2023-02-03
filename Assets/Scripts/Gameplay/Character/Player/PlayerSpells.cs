using System;
using System.Collections;
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
            if ( ! Lazy.Resources.TrySpendHeat(spellToUse.Spell.HeatCost))
                return;
            spellToUse.OnCast();
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

        private async void LoadSpellBehaviour(int index)
        {
            Spell spell = Kit.Eqipped[index];
            GameObject prefab = (await spell.Behaviour.LoadAssetAsync<GameObject>().Task); 
            SpellBehaviour behaviour = ContainerFactory.Instantiate<SpellBehaviour>(prefab, Transform.position, Transform);
            behaviour.Init(spell, Lazy);
            _spellBehaviours[index] = behaviour;
            spell.Behaviour.ReleaseAsset();
            OnSpellLoaded?.Invoke(index, behaviour);
        }
    }
}