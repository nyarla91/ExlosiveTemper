using System.Linq;
using Extentions;
using Gameplay.Character;
using UnityEngine;

namespace Gameplay
{
    public class AreaOfEffect : MonoBehaviour
    {
        public static Hitbox[] GetTargets(Vector3 position, float radius, LayerMask mask)
        {
            Collider[] enemiesInside = Physics.OverlapSphere(position.WithY(1.5f), radius, mask);
            return enemiesInside.Select(enemy => enemy.GetComponent<Hitbox>()).ToArray();
        }
    }
}