using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class TutorialsBase : MonoBehaviour
    {
        private SavedTutorials _savedTutorials;


        private string SavefilePath => Application.dataPath + "/tutorials.json";
        
        public bool TrySeeTutorial(string tutorialName)
        {
            if (_savedTutorials.Seen.Contains(tutorialName))
                return false;
            
            _savedTutorials.Seen.Add(tutorialName);
            File.WriteAllText(SavefilePath, JsonUtility.ToJson(_savedTutorials));
            return true;
        }

        private void Awake()
        {
            if (!File.Exists(SavefilePath))
            {
                _savedTutorials = new SavedTutorials(new List<string>());
                return;
            }
            string json = File.ReadAllText(SavefilePath);
            _savedTutorials = JsonUtility.FromJson<SavedTutorials>(json);
        }

        [Serializable]
        private class SavedTutorials
        {
            [SerializeField] private List<string> _seen;
            
            public List<string> Seen => _seen;

            public SavedTutorials(List<string> seen)
            {
                _seen = seen;
            }
        }
    }
    
}