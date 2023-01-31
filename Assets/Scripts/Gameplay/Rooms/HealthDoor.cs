using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.Rooms
{
    public class HealthDoor : Door
    {
        [SerializeField] private int _cost;
        
        protected override bool CanBeOpen(PlayerComposition player) => true;

        protected override void OpenEffect(PlayerComposition player) => player.Vitals.TakeDamage(_cost);
    }
}