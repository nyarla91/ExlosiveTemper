using UnityEngine;
using Zenject;

namespace Achievements
{
    public class AchievementActivator : MonoBehaviour
    {
        [Inject] private SpellUnlocks Unlocks { get; set; }

        private void Start()
        {
            Unlocks.InstantiateAchievements();
        }

        private void OnDestroy()
        {
            Unlocks.DisposeAchievements();
        }
    }
}