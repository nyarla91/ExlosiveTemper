using System;
using Extentions;
using Extentions.Factory;
using Gameplay.PostProcessing;
using UnityEngine;
using Zenject;

namespace Gameplay.VFX
{
    public class Explosion : ParticleSystemEffect
    {
        [Inject] private Shake Shake { get; set; }

        protected override void Start()
        {
            base.Start();
            Shake.AddImpulse(Transform.localScale.magnitude);
        }

        public static void CreateExplosion(ContainerFactory factory, GameObject prefab, Vector3 position, float radius)
        {
            Explosion explosion = factory.Instantiate<Explosion>(prefab, position.WithY(1));
            explosion.Transform.localScale = Vector3.one * radius * 0.75f;
        }
    }
}