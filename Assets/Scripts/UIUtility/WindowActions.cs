using System;
using UnityEngine;

namespace UIUtility
{
    public class WindowActions : MenuAdditionalActions<MenuWindow>
    {
        protected override Func<MenuWindow, bool> TriggerCondition => window => window.IsOpened;
    }
}