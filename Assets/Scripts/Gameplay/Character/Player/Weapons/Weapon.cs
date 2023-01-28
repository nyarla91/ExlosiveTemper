﻿using System;
using Extentions;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Character.Player.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private PlayerWeapons _player;
        [SerializeField] private WeaponAttack _primaryAttack;
        [SerializeField] private WeaponAttack _chargedAttack;
        [SerializeField] private float _attackPeriod;

        private Timer _cooldown;
        
        [Inject] private Pause Pause { get; set; }

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
                Ray ray = new Ray(_player.Transform.position, direction.RotatedY(degreeOffset));
                LayerMask mask = LayerMask.GetMask("Enemy", "Obstacle");
                
                if ( ! Physics.Raycast(ray, out RaycastHit raycastHit, 50, mask))
                    continue;

                Hitbox target = raycastHit.collider.GetComponent<Hitbox>();
                if (target == null)
                    continue;
                
                _player.OnHit?.Invoke(target.TakeHit(1));
            }
        }

        private void Start()
        {
            _cooldown = new Timer(this, _attackPeriod, Pause).Start();
        }
    }
}