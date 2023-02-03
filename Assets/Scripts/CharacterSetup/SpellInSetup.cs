using System.Linq;
using Achievements;
using Content;
using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CharacterSetup
{
    public class SpellInSetup : MonoBehaviour
    {
        [SerializeField] private Spell _spell;
        [SerializeField] private Image _iconUnderlay;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private LocalizedTextMesh _name;
        [SerializeField] private LocalizedTextMesh _description;

        private bool _isEqipped;

        public bool IsEqipped
        {
            get => _isEqipped;
            set
            {
                _isEqipped = value;
                _iconUnderlay.color = value ? Color.green : Color.white;
            }
        }
        
        private bool IsAvailable { get; set; }

        private SpellsKit Kit { get; set; }

        private SpellUnlocks Unlocks { get; set; }

        [Inject]
        private void Construct(SpellsKit kit, SpellUnlocks unlocks)
        {
            Kit = kit;
            Unlocks = unlocks;
        }

        public void ToggleThisSpellEquipped()
        {
            if ( ! IsAvailable)
                return;
            if (IsEqipped)
                Kit.TryUnequipSpell(_spell);
            else
                Kit.TryEquipSpell(_spell);
        }

        private void UpdateEquipped(Spell[] spells)
        {
            IsEqipped = spells.Contains(_spell);
        }

        private void ApplyView()
        {
            _icon.sprite = _spell.Icon;
            _cost.text = $"{_spell.HeatCost}<sprite name=heat>";
            if (_spell.Achievement != null && _spell.Achievement.IsAvailable(Unlocks.SavableSpells))
            {
                _name.Text = new LocalizedString("Locked", "Закрыто");
                _description.Text = _spell.Achievement.Description;
                _icon.color = new Color(1, 1, 1, 0.4f);
                IsAvailable = false;
                return;
            }
            IsAvailable = true;
            _name.Text = _spell.Name;
            _description.Text = _spell.Description;
        }

        private void Start()
        {
            ApplyView();
            Kit.SpellsUpdated += UpdateEquipped;
            UpdateEquipped(Kit.Eqipped);
        }

        private void OnDestroy()
        {
            Kit.SpellsUpdated -= UpdateEquipped;
        }
    }
}