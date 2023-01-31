using Extentions;
using Gameplay.Character.Player;
using Gameplay.Interactable;
using UnityEngine;

namespace Gameplay.Rooms
{
    public abstract class Door : Transformable, IInteractable
    {
        [SerializeField] private Collider _collider;
        
        public void OnInteract(PlayerComposition player)
        {
            if ( ! CanBeOpen(player))
                return;
            OpenEffect(player);
            Open();
        }

        public void Open()
        {
            _collider.enabled = false;
        }

        public void Lock()
        {
            _collider.enabled = true;
        }

        protected abstract bool CanBeOpen(PlayerComposition player);
        protected abstract void OpenEffect(PlayerComposition player);
    }
}