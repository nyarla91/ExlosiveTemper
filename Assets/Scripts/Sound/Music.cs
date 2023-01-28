using DG.Tweening;
using Extentions;
using UnityEngine;

namespace Sound
{
    public class Music : LazyGetComponent<AudioSource>
    {
        public void Play(AudioClip clip)
        {
            Lazy.clip = clip;
            Lazy.Play();
            Lazy.DOKill();
            Lazy.DOFade(1, 1);
        }

        public void Stop()
        {
            Lazy.DOKill();
            Lazy.DOFade(0, 1);
        }
    }
}