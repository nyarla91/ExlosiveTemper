using UIUtility;
using UnityEngine;
using Zenject;

namespace Progression
{
    public class SaveSpellsButton : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        [SerializeField] private MenuWindow _setupWindow;
        [SerializeField] private MenuWindow _errorMessageWindow;
        [Inject] private SpellsKit Kit { get; set; }
        
        public void SaveAndExit()
        {
            if (Kit.HasEmptySlots)
            {
                _menu.SwitchToWindow(_errorMessageWindow);
            }
            else
            {
                Kit.SaveEquipped();
                _setupWindow.OpenPreviousMenu();
            }
        }
    }
}