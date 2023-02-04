using Extentions;
using Gameplay.Character.Player;
using Localization;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public abstract class Interactable : Transformable
    {
        [SerializeField] private LocalizedString _context;

        [Inject] protected Pause Pause { get; set; }
        
        public LocalizedString Context => _context;
        public abstract bool IsInteractableAtTheMoment { get; }
        
        public abstract void OnInteract(PlayerComposition player);
    }
}