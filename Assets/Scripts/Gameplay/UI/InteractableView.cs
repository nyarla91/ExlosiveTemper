using DG.Tweening;
using Extentions;
using Gameplay.Character.Player;
using Localization;
using UI;
using UIUtility;
using UnityEngine;

namespace Gameplay.UI
{
    public class InteractableView : Transformable
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private LocalizedTextMesh _localizedTmp;
        
        public void Init(PlayerInteraction interaction)
        {
            interaction.OnInteractionAvailable += Show;
            interaction.OnInteractionUnavailable += Hide;
        }

        private void Show(Interactable interactable)
        {
            _localizedTmp.Text = interactable.Context;
            RectTransform.DOAppear(_canvasGroup);
        }

        private void Hide(Interactable _)
        {
            RectTransform.DODisappear(_canvasGroup);
        }
    }
}