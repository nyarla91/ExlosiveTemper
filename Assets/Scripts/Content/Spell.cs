using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content
{
    [CreateAssetMenu(menuName = "Spell")]
    public class Spell : ScriptableObject
    {
        [field: SerializeField] public float CastTime { get; set; }
        [field: SerializeField] public int HeatCost { get; set; }
        [field: SerializeField] public AssetReference Behaviour { get; set; }
    }
}