using System.IO;
using System.Linq;
using UnityEngine;

namespace Save
{
    public class Save : MonoBehaviour
    {
        [SerializeField] private SaveData _standartSaveData;

        private SaveData _saveData;
        
        private string SaveFilePath => Application.dataPath + "/save.json";

        public int[] EquippedSpells => _saveData.EquippedSpells.ToArray();
        public string[] CompleteAchievements => _saveData.CompleteAchievements.ToArray();

        public bool TrySeeTutorial(string tutorial)
        {
            if (_saveData.SeenTutorials.Contains(tutorial))
                return false;
            _saveData.SeenTutorials.Add(tutorial);
            SaveFile();
            return true;
        }
        
        public void AddCompleteAchievement(string achievement)
        {
            _saveData.CompleteAchievements.Add(achievement);
            SaveFile();
        }
        
        public void UpdateEquippedSpells(int[] spells)
        {
            _saveData.EquippedSpells = spells;
            SaveFile();
        }
        
        private void SaveFile()
        {
            File.WriteAllText(SaveFilePath, JsonUtility.ToJson(_saveData));
        }

        private void Awake()
        {
            _saveData = File.Exists(SaveFilePath)
                ? JsonUtility.FromJson<SaveData>(File.ReadAllText(SaveFilePath))
                : _standartSaveData;
        }
    }
}