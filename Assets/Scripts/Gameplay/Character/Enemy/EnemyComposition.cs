using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyComposition : Transformable
    {
        [Inject] public PlayerMovement Player { get; private set; }

        public Vector3 DirectionToPlayer => Transform.DirectionTo(Player.Transform).WithY(0).normalized;
    }
}