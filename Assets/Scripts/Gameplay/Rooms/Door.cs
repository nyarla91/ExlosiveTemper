using DG.Tweening;
using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Rooms
{
    public abstract class Door : Interactable
    {
        [SerializeField] private Collider _collider;

        public override bool IsInteractableAtTheMoment => ! EnemySpawner.IsCombatOn;
        
        [Inject] private EnemySpawner EnemySpawner { get; set; }

        public override void OnInteract(PlayerComposition player)
        {
            if (Pause.IsPaused || ! CanBeOpen(player))
                return;
            OpenEffect(player);
            Open();
        }

        public void Open()
        {
            Transform.DOComplete();
            Transform.DOLocalMoveY(-10, 1);
        }

        public void Lock()
        {
            Transform.DOKill();
            Transform.localPosition = Transform.localPosition.WithY(0);
            _collider.enabled = true;
        }

        protected abstract bool CanBeOpen(PlayerComposition player);
        protected abstract void OpenEffect(PlayerComposition player);
    }
}