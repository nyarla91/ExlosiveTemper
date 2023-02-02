using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Content
{
    [CreateAssetMenu(menuName = "Enemy Spawn", order = 0)]
    public class EnemySpawnDetails : ScriptableObject
    {
        [field: SerializeField] public int Weight { get; private set; }
        [field: SerializeField] public GameObject BasePrefab { get; private set; }
        [field: SerializeField] public GameObject ElitePrefab { get; private set; }
    }
}