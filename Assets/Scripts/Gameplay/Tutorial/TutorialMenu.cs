using UIUtility;
using UnityEngine;
using Zenject;

namespace Gameplay.Tutorial
{
    public class TutorialMenu : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        [SerializeField] private string _name;
        
        [Inject] private Save.Save Save { get; set; }
        
        protected void TryShowTutorial()
        {
            if (Save.TrySeeTutorial(_name))
                _menu.Open();
        }
    }
}