using Extentions;
using Gameplay.Character.Player;
using Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyComposition : Transformable
    {
        private VitalsPool _vitalsPool;
        private EnemyStatus _status;

        public VitalsPool VitalsPool => _vitalsPool ??= GetComponent<VitalsPool>();
        public EnemyStatus Status => _status ??= GetComponent<EnemyStatus>();
        
        public PlayerComposition Player { get; set; }

        public Vector3 DirectionToPlayer => Transform.DirectionTo(Player.Transform).WithY(0).normalized;
    }
}