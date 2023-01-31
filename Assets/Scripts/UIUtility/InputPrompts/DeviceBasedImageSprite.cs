using UnityEngine;
using UnityEngine.UI;

namespace UIUtility.InputPrompts
{
    public class DeviceBasedImageSprite : DeviceBasedInputPrompts<Image, Sprite>
    {
        protected override void ApplyPrompt(Sprite prompt) => Graphics.sprite = prompt;
    }
}