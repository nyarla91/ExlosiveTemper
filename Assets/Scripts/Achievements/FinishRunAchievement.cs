using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Achievements
{
    public class FinishRunAchievement : AchievementBehaviour
    {
        [Inject] private PlayerComposition Player { get; set; }
        
        private void Start()
        {
            Player.Vitals.HealthIsOver += Complete;
        }
    }
}