using System.Linq;
using Extentions;
using Gameplay.Character;
using UnityEngine;

namespace Gameplay
{
    public class AOE : MonoBehaviour
    {
        public static T[] GetTargets<T>(Vector3 position, float radius, LayerMask mask)
        {
            Collider[] colliders = Physics.OverlapSphere(position.WithY(1.5f), radius, mask);
            return colliders.Select(t => t.GetComponent<T>()).Where(t => t != null).ToArray();
        }
    }
}