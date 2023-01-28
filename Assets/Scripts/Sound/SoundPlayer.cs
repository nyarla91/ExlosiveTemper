using Extentions.Factory;
using UnityEngine;

namespace Sound
{
    public class SoundPlayer : PoolFactory
    {
        [SerializeField] private float _volume;
        
        private Camera _mainCamera;
        private Camera MainCamera => _mainCamera ??= Camera.main;
        
        public void Play(AudioClip clip, float volumeScale)
        {
            SoundInstance instance = GetNewObject<SoundInstance>(MainCamera.transform.position);
            instance.Init(clip, volumeScale * _volume);
        }
    }
}
