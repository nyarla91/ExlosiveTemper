using UnityEngine.UI;

namespace UIUtility.UIElements
{
    public class UIButton : Button
    {
        public void DoPressedTransition() => DoStateTransition(SelectionState.Pressed, false);
        public void DoNoTransition() => DoStateTransition(SelectionState.Normal, false);
    }
}