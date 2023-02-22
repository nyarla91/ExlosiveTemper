using System;
using System.Linq;
using Extentions;
using Extentions.Factory;
using Gameplay.Character;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int _animationIndex;
        [SerializeField] private PlayerWeapons _player;
        [SerializeField] private WeaponAttack _primaryAttack;
        [SerializeField] private WeaponAttack _chargedAttack;
        [SerializeField] private float _attackPeriod;
        [SerializeField] private GameObject _impactPrefab;
        [SerializeField] private Transform _effectOrigin;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _adaptiveTriggersForce;

        private Timer _cooldown;

        public int AnimationIndex => _animationIndex;
        public float AdaptiveTriggersForce => _adaptiveTriggersForce;
        public bool IsOnCooldown => _cooldown.IsOn;
        [Inject] private ContainerFactory Factory { get; set; }
        [Inject] private Pause Pause { get; set; }

        public event Action<WeaponAttack, Vector3, Hitbox[]> HitscanBulletShot; 

        public bool TryShoot()
        {
            if (_cooldown.IsOn)
                return false;

            PerfromHitscanAttack(_primaryAttack, _player.Transform.forward);
            _cooldown.Restart();
            CreateImpactEffect();
            return true;
        }

        private void CreateImpactEffect()
        {
            Transform effect = Factory.Instantiate<Transform>(_impactPrefab, _effectOrigin.position, _effectOrigin);
            effect.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public bool TryChargedShot()
        {
            PerfromHitscanAttack(_chargedAttack, _player.Transform.forward);
            CreateImpactEffect();
            return true;
        }

        private void PerfromHitscanAttack(WeaponAttack attack, Vector3 direction)
        {
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(attack.Sound);
            
            for (int i = 0; i < attack.ShotsPerAttack; i++)
            {
                float degreeOffset = Random.Range(-attack.SplashAmplitude, attack.SplashAmplitude);
                Ray ray = new Ray(_player.Transform.position.WithY(1.5f), direction.RotatedY(degreeOffset));
                LayerMask mask = LayerMask.GetMask("Enemy", "Obstacle");

                RaycastHit[] hits;
                if (attack.PiercesEnemies)
                {
                    hits = Physics.RaycastAll(ray.origin, ray.direction, 50, mask);
                    HitscanBulletShot?.Invoke(attack, hits.First().point,
                        hits.Select(hit => hit.collider.GetComponent<Hitbox>()).ToArray());
                }
                else
                {
                    if ( ! Physics.Raycast(ray, out RaycastHit raycastHit, 50, mask))
                        continue;
                    hits = new[] {raycastHit};
                    HitscanBulletShot?.Invoke(attack, raycastHit.point, new []{raycastHit.collider.GetComponent<Hitbox>()});
                }
                foreach (RaycastHit raycastHit in hits)
                {
                    Hitbox target = raycastHit.collider.GetComponent<Hitbox>();
                    if (target == null)
                        continue;
                    
                    _player.Hit?.Invoke(target.TakeHit(attack.DamagePerAttack / attack.ShotsPerAttack));    
                }
            }
        }

        private void OnEnable()
        {
            _cooldown?.Restart();
        }

        private void Start()
        {
            _cooldown = new Timer(this, _attackPeriod, Pause).Start();
        }
    }
}