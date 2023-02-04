using Extentions;
using Extentions.Factory;
using UnityEngine;

namespace Gameplay.VFX
{
    public class Explosion : ParticleSystemEffect
    {
        public static void CreateExplosion(ContainerFactory factory, GameObject prefab, Vector3 position, float radius)
        {
            Explosion explosion = factory.Instantiate<Explosion>(prefab, position.WithY(1));
            explosion.Transform.localScale = Vector3.one * radius * 0.75f;
        }
    }
}