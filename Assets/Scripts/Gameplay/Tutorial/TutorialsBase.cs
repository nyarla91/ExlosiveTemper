using System;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class TutorialsBase : MonoBehaviour
    {
        private SavedTutorials _savedTutorials;
        
        
        public bool IsTutorialSeen(string tutorialName)
        {
            return true;
        }

        [Serializable]
        private class SavedTutorials
        {
            [SerializeField] private string[] _names;
            
            public string[] Names => _names;

            public SavedTutorials(string[] names)
            {
                _names = names;
            }
        }
    }
    
}