using Extentions;
using Gameplay.Interactable;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerInteractable : LazyGetComponent<PlayerComposition>
    {
        private IInteractable _activeInterractabe;

        private void Awake()
        {
            Lazy.Controls.OnInteract += TryInteract;
        }

        private void TryInteract() => _activeInterractabe?.OnInteract(Lazy);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                _activeInterractabe = interactable;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable) && interactable == _activeInterractabe )
            {
                _activeInterractabe = null;
            }
        }
    }
}