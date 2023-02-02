using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerComposition : Transformable
    {
        private PlayerControls _controls;
        private PlayerMovement _movement;
        private PlayerSight _sight;
        private PlayerAnimation _animation;
        private PlayerWeapons _weapons;
        private PlayerResources _resources;
        private PlayerInventory _inventory;
        private PlayerSpells _spells;
        private StateMachine _stateMachine;
        private VitalsPool _vitals;
        private Hitbox _hitbox;
        public PlayerControls Controls => _controls ??= GetComponent<PlayerControls>();
        public PlayerMovement Movement => _movement ??= GetComponent<PlayerMovement>();
        public PlayerAnimation Animation => _animation ??= GetComponent<PlayerAnimation>();
        public PlayerSight Sight => _sight ??= GetComponent<PlayerSight>();
        public PlayerResources Resources => _resources ??= GetComponent<PlayerResources>();
        public PlayerWeapons Weapons => _weapons ??= GetComponent<PlayerWeapons>();
        public PlayerInventory Inventory => _inventory ??= GetComponent<PlayerInventory>();

        public PlayerSpells Spells => _spells ??= GetComponent<PlayerSpells>();
        public StateMachine StateMachine => _stateMachine ??= GetComponent<StateMachine>();
        public VitalsPool Vitals => _vitals ??= GetComponent<VitalsPool>();
        public Hitbox Hitbox => _hitbox ??= GetComponent<Hitbox>();
        
        [Inject] public CameraView CameraView { get; private set; }
    }
}