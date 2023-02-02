using System;
using Extentions;
using Gameplay.Character.Player;
using Gameplay.Spells;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class SpellView : MonoBehaviour
    {
        [SerializeField] private int _spellIndex;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _cost;
        
        private PlayerResources _resources;
        private SpellBehaviour _spellBehaviour;

        public void Init(PlayerResources resources, PlayerSpells spell)
        {
            _resources = resources;
            spell.OnSpellLoaded += InitSpell;
        }

        private void InitSpell(int index, SpellBehaviour spellBehaviour)
        {
            if (index != _spellIndex)
                return;
            _spellBehaviour = spellBehaviour;
            _cost.text = _spellBehaviour.Spell.HeatCost.ToString();
            _icon.sprite = _spellBehaviour.Spell.Icon;
        }

        private void Update()
        {
            if (_spellBehaviour != null)
            {
                _icon.SetAlpha(_resources.Heat.Value >= _spellBehaviour.Spell.HeatCost ? 1 : 0.3f);
            }
        }
    }
}