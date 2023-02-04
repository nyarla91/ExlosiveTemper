using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class BeginningTutorial : TutorialMenu
    {
        private async void Start()
        {
            print("LEE");
            await Task.Delay(500);
            TryShowTutorial();
        }
    }
}