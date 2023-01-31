using UnityEngine;

namespace UIUtility.InputPrompts
{
    public class DeviceBasedMaterial : DeviceBasedInputPrompts<Renderer, Material>
    {
        protected override void ApplyPrompt(Material prompt) => Graphics.material = prompt;
    }
}