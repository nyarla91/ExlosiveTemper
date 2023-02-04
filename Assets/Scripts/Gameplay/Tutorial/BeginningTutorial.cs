using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class BeginningTutorial : TutorialMenu
    {
        private async void Start()
        {
            await Task.Delay(500);
            TryShowTutorial();
        }
    }
}