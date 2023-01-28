using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerComposition : MonoBehaviour
    {
        private PlayerControls _controls;
        private PlayerMovement _movement;
        private PlayerSight _sight;
        private PlayerAnimation _animation;
        private PlayerWeapons _weapons;
        private PlayerResources _resources;
        private StateMachine _stateMachine;
        private VitalsPool _vitals;
        public PlayerControls Controls => _controls ??= GetComponent<PlayerControls>();
        public PlayerMovement Movement => _movement ??= GetComponent<PlayerMovement>();
        public PlayerAnimation Animation => _animation ??= GetComponent<PlayerAnimation>();
        public PlayerSight Sight => _sight ??= GetComponent<PlayerSight>();
        public PlayerResources Resources => _resources ??= GetComponent<PlayerResources>();
        public PlayerWeapons Weapons => _weapons ??= GetComponent<PlayerWeapons>();
        public StateMachine StateMachine => _stateMachine ??= GetComponent<StateMachine>();
        public VitalsPool Vitals => _vitals ??= GetComponent<VitalsPool>();
        
        [Inject] public CameraView CameraView { get; private set; }
    }
}