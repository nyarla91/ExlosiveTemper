using System;
using System.Linq;
using Extentions;
using Gameplay.Character;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject _aim;
        [SerializeField] private int _animationIndex;
        [SerializeField] private PlayerWeapons _player;
        [SerializeField] private WeaponAttack _primaryAttack;
        [SerializeField] private WeaponAttack _chargedAttack;
        [SerializeField] private float _attackPeriod;

        private Timer _cooldown;

        public int AnimationIndex => _animationIndex;

        public GameObject Aim => _aim;
        [Inject] private Pause Pause { get; set; }

        public event Action<WeaponAttack, Vector3, Hitbox[]> HitscanBulletShot; 

        public bool TryShoot()
        {
            if (_cooldown.IsOn)
                return false;

            PerfromHitscanAttack(_primaryAttack, _player.Transform.forward);
            _cooldown.Restart();
            return true;
        }

        public bool TryChargedShot()
        {
            PerfromHitscanAttack(_chargedAttack, _player.Transform.forward);
            return true;
        }

        private void PerfromHitscanAttack(WeaponAttack attack, Vector3 direction)
        {
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

        private void Start()
        {
            _cooldown = new Timer(this, _attackPeriod, Pause).Start();
        }
    }
}