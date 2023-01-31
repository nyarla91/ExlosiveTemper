using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.Rooms
{
    public class HeatDoor : Door
    {
        [SerializeField] private int _cost;
        
        protected override bool CanBeOpen(PlayerComposition player) => player.Resources.Heat.Value >= _cost;

        protected override void OpenEffect(PlayerComposition player) => player.Resources.WasteHeat(_cost);
    }
}