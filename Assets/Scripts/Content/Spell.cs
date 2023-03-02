using Localization;
using UnityEngine;

namespace Content
{
    [CreateAssetMenu(menuName = "Spell")]
    public class Spell : ScriptableObject
    {
        [field: SerializeField] public LocalizedString Name { get; private set; }
        [field: SerializeField] public LocalizedString Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int HeatCost { get; private set; }
        [field: SerializeField] public GameObject Behaviour { get; private set; }
        public bool IsUnlocked { get; private set; } = true;
        public LocalizedString UnlockCondition { get; private set; }

        public void Lock(LocalizedString unlockCondition)
        {
            IsUnlocked = false;
            UnlockCondition = unlockCondition;
        }

        public void Unlock() => IsUnlocked = true;
    }
}