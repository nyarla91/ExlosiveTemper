using Extentions;
using Gameplay.Character.Player;
using Localization;
using UnityEngine;

namespace Gameplay
{
    public abstract class Interactable : Transformable
    {
        [SerializeField] private LocalizedString _context;

        public LocalizedString Context => _context;
        
        public abstract void OnInteract(PlayerComposition player);
    }
}