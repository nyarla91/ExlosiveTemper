using UIUtility;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class TutorialMenu : Menu
    {
        [SerializeField] private string _name;
        
        protected void Show()
        {
            Open();
        }
    }
}