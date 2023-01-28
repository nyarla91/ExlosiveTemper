using Extentions;
using UnityEngine;

namespace Gameplay
{
    public class CameraView : Transformable
    {
        [SerializeField] private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;
        
        public Vector3 ScreenToPerspective(Vector2 screen) => Transform.forward.WithY(0).normalized * screen.y + Transform.right * screen.x;
    }
}