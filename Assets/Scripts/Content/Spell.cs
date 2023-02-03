using Achievements;
using Localization;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content
{
    [CreateAssetMenu(menuName = "Spell")]
    public class Spell : ScriptableObject
    {
        [field: SerializeField] public LocalizedString Name { get; set; }
        [field: SerializeField] public LocalizedString Description { get; set; }
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public float CastTime { get; set; }
        [field: SerializeField] public int HeatCost { get; set; }
        [field: SerializeField] public AssetReference Behaviour { get; set; }
        [field: SerializeField] public Achievement Achievement { get; private set; }
    }
}