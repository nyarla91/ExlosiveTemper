using System;
using System.Collections;
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
        [SerializeField] private Spell[] _spells;

        private SpellBehaviour[] _spellBehaviours;
        private Coroutine _castCoroutine;
        private SpellBehaviour _currentlyCastedSpell;

        public SpellBehaviour[] SpellBehaviours => _spellBehaviours;

        [Inject] private ContainerFactory ContainerFactory { get; set; }

        public event Action<int, SpellBehaviour> OnSpellLoaded;
        
        private void Awake()
        {
            Lazy.Controls.OnSpellUse += TryUseSpell;
            Lazy.StateMachine.GetState(StateMachine.Performing).OnExit += TryInterruptCast;
        }

        private void TryInterruptCast()
        {
            if (Lazy.StateMachine.IsCurrentStateOneOf(StateMachine.Performing))
                return;
            if (_currentlyCastedSpell is IContiniousSpell continiousSpell)
                continiousSpell.OnInterruptCast();
            OnCastEndOrInterrupt();
        }

        private void TryUseSpell(int index)
        {
            SpellBehaviour spellToUse = _spellBehaviours[index];
            if ( ! Lazy.StateMachine.CurrentState.CanSwitchToState(StateMachine.Performing)
                 || Lazy.Resources.Heat.Value < spellToUse.Spell.HeatCost)
                return;
            _castCoroutine = StartCoroutine(SpellUsage(spellToUse));
        }

        private IEnumerator SpellUsage(SpellBehaviour spellBehaviour)
        {
            if (!Lazy.StateMachine.TryEnterState(StateMachine.Performing))
                yield break;

            _currentlyCastedSpell = spellBehaviour;
            Spell spell = spellBehaviour.Spell;
            IContiniousSpell continiousSpell = spellBehaviour is IContiniousSpell behaviour ? behaviour : null;
            
            continiousSpell?.OnCastStart();
            for (float i = 0; i < spell.CastTime; i += Time.fixedDeltaTime)
            {
                Lazy.Resources.WasteHeat(spell.HeatCost / spell.CastTime * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            spellBehaviour.OnEndCast();
            OnCastEndOrInterrupt();
        }

        private void OnCastEndOrInterrupt()
        {
            _castCoroutine?.Stop(this, ref _castCoroutine);
            _currentlyCastedSpell = null;
            Lazy.StateMachine.TryExitState(StateMachine.Performing);
        }

        private void Start()
        {
            _spellBehaviours = new SpellBehaviour[_spells.Length];
            for (var i = 0; i < _spells.Length; i++)
            {
                LoadSpellBehaviour(i);
            }
        }

        private async void LoadSpellBehaviour(int index)
        {
            Spell spell = _spells[index];
            GameObject prefab = (await spell.Behaviour.LoadAssetAsync<GameObject>().Task); 
            SpellBehaviour behaviour = ContainerFactory.Instantiate<SpellBehaviour>(prefab, Transform.position, Transform);
            behaviour.Init(spell, Lazy);
            _spellBehaviours[index] = behaviour;
            spell.Behaviour.ReleaseAsset();
            OnSpellLoaded?.Invoke(index, behaviour);
        }
    }
}