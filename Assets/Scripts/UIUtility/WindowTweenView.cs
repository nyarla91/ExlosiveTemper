using Extentions;
using UnityEngine;

namespace UIUtility
{
    public class WindowTweenView : Transformable
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public void OnOpen()
        {
            RectTransform.DOAppear(_canvasGroup);
        }

        public void OnClose()
        {
            RectTransform.DODisappear(_canvasGroup);
        }
    }
}