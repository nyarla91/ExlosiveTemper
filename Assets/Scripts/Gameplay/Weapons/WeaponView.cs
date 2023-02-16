using System;
using Extentions;
using Gameplay.Character;
using Gameplay.VFX;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponView : Transformable
    {
        [SerializeField] private GameObject _shotTrailPrefab;
        [SerializeField] private Weapon _weapon;

        private void Awake()
        {
            _weapon.HitscanBulletShot += CreateVisualEffect;
        }

        private void CreateVisualEffect(WeaponAttack attack, Vector3 target, Hitbox[] _)
        {
            Vector3 position = Transform.position;
            ShotTrail trail = Instantiate(_shotTrailPrefab, position, Quaternion.identity).GetComponent<ShotTrail>();
            trail.Init(position, target, 0.1f);
        }
    }
}