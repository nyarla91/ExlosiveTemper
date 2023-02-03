using Extentions;
using UnityEngine;
using Zenject;

namespace Achievements
{
    public class FinishRunAchievement : AchievementBehaviour
    {
        [SerializeField] private int _seconds;
        private Timer _timer;
        
        [Inject] private Pause Pause { get; set; }
        
        private void Start()
        {
            _timer = new Timer(this, _seconds, Pause).Start();
            _timer.Expired += Complete;
        }
    }
}