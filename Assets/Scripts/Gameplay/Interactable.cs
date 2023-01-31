using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.Interactable
{
    public interface IInteractable
    {
        public void OnInteract(PlayerComposition player);
    }
}