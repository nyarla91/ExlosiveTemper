using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerInteraction : LazyGetComponent<PlayerComposition>
    {
        private Interactable _activeInterractabe;

        public event Action<Interactable> OnInteractionAvailable; 
        public event Action<Interactable> OnInteractionUnavailable; 
        
        private void Awake()
        {
            Lazy.Controls.OnInteract += TryInteract;
        }

        private void TryInteract() => _activeInterractabe?.OnInteract(Lazy);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable) && interactable.IsInteractableAtTheMoment)
            {
                _activeInterractabe = interactable;
                OnInteractionAvailable?.Invoke(interactable);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable) && interactable == _activeInterractabe )
            {
                _activeInterractabe = null;
                OnInteractionUnavailable?.Invoke(interactable);
            }
        }
    }
}