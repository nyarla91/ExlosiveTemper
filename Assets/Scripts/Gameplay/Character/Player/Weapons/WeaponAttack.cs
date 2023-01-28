using System;
using UnityEngine;

namespace Gameplay.Character.Player.Weapons
{
    [Serializable]
    public class WeaponAttack
    {
        [field: SerializeField] public int ShotsPerAttack { get; private set; }
        [field: SerializeField] public float SplashAmplitude { get; private set; }
    }
}