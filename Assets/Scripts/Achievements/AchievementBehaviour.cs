using UnityEngine;
using Zenject;

namespace Achievements
{
    public class AchievementBehaviour : MonoBehaviour
    {
        public Achievement Achievement { get; private set; }
        [Inject] private SpellUnlocks Unlocks { get; set; }
        
        public void Init(Achievement achievement)
        {
            Achievement = achievement;
        }

        protected void Complete()
        {
            Unlocks.UnlockSpell(Achievement.UnlockedSpell);
        }
    }
}