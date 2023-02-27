using System.Collections;
using Extentions;
using Gameplay.Projectiles;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Character.Enemy
{
    public class EnemyDefaultAttackPattern : EnemyAttackPattern
    {
        [SerializeField] private float _wavesPeriod;
        [SerializeField] private int _burstsPerWave;
        [SerializeField] private float _burstsPeriod;
        [SerializeField] private int _projectilesPerBurst;
        [SerializeField] private float _splashAmplitude;
        [SerializeField] private bool _randomSplash;
        [SerializeField] private float _projectileSpeed;

        [Inject] private Pause Pause { get; set; }
        
        private void Start()
        {
            StartCoroutine(ShootCycle());
        }

        private IEnumerator ShootCycle()
        {
            yield return new PausableWaitForSeconds(this, Pause, 1);
            yield return new PausableWaitForSeconds(this, Pause, Random.Range(0, _wavesPeriod));
            while (true)
            {
                for (int burst = 0; burst < _burstsPerWave; burst++)
                {
                    for (int projectile = 0; projectile < _projectilesPerBurst; projectile++)
                    {
                        float angleOffset;
                        if ( ! _randomSplash && _projectilesPerBurst > 1)
                        {
                            float t = projectile / (float) (_projectilesPerBurst - 1);
                            angleOffset = MathExtentions.EvaluateLine(-_splashAmplitude, _splashAmplitude, t);
                        }
                        else
                        {
                            angleOffset = Random.Range(-_splashAmplitude, _splashAmplitude);
                        }
                        SpawnProjectile(Lazy.DirectionToPlayer.RotatedY(angleOffset) * _projectileSpeed);
                    }
                    yield return new PausableWaitForSeconds(this, Pause, _burstsPeriod);
                }
                yield return new PausableWaitForSeconds(this, Pause, _wavesPeriod);
            }
        }
    }
}