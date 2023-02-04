using Gameplay.Rooms;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class LevelTutorial : TutorialMenu
    {
        [SerializeField] private Room _room;
        [SerializeField] private int _level;

        private void Awake()
        {
            _room.ComeToNextLevel += level =>
            {
                if (_level == level)
                    TryShowTutorial();
            };
        }
    }
}