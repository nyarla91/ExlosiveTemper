using System.Collections;
using DG.Tweening;
using Extentions;
using Localization;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Achievements
{
    public class AchievementMessage : Transformable
    {
        [SerializeField] private float _duration;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _icon;
        [SerializeField] private LocalizedTextMesh _description;
        
        public void Show(Achievement achievement)
        {
            _description.Text = achievement.Description;
            _icon.sprite = achievement.UnlockedSpell.Icon;
            Transform.DOComplete();
            Transform.DOAppear(_canvasGroup);
            StartCoroutine(Hide());
        }

        private IEnumerator Hide()
        {
            yield return new WaitForSeconds(_duration);
            Transform.DODisappear(_canvasGroup);
        }
    }
}