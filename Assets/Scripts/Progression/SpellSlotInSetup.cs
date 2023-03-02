using Content;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Progression
{
    public class SpellSlotInSetup : MonoBehaviour
    {
        [SerializeField] private int _slotNumber;
        [SerializeField] private Image _icon;
        
        [Inject] private SpellsKit Kit { get; set; }

        public void UnequipThisSlot() => Kit.TryUnequipSpellInSlot(_slotNumber);

        private void Start()
        {
            Kit.SpellsUpdated += UpdateIcon;
            UpdateIcon(Kit.Eqipped);
        }

        private void UpdateIcon(Spell[] spells)
        {
            bool isSlotEquipped = spells[_slotNumber] != null;
            _icon.color = isSlotEquipped ? Color.white : Color.clear;
            if (isSlotEquipped)
                _icon.sprite = spells[_slotNumber].Icon;
        }

        private void OnDestroy()
        {
            Kit.SpellsUpdated -= UpdateIcon;
        }
    }
}