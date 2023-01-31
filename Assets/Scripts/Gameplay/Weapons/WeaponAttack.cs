using System;
using UnityEngine;

namespace Gameplay.Weapons
{
    [Serializable]
    public class WeaponAttack
    {
        [field: SerializeField] public float DamagePerAttack { get; private set; }
        [field: SerializeField] public int ShotsPerAttack { get; private set; }
        [field: SerializeField] public float SplashAmplitude { get; private set; }
    }
}