using System.Collections;
using Extentions.Factory;
using UnityEngine;

namespace Sound
{
    public class SoundInstance : PooledObject
    {
        private AudioSource _source;

        public AudioSource Source => _source ??= GetComponent<AudioSource>();
        
        public void Init(AudioClip clip, float volumeScale)
        {
            Source.clip = clip;
            Source.PlayOneShot(clip, volumeScale);
            StartCoroutine(Dispose());
        }

        private IEnumerator Dispose()
        {
            yield return new WaitForSeconds(5);
            PoolDisable();
        }
    }
}