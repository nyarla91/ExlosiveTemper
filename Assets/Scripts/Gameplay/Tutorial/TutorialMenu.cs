using UIUtility;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class TutorialMenu : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        [SerializeField] private TutorialsBase _base;
        [SerializeField] private string _name;
        
        protected void TryShowTutorial()
        {
            if (_base.TrySeeTutorial(_name))
                _menu.Open();
        }
    }
}