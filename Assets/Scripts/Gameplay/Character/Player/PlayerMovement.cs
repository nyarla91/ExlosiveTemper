using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerMovement : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _sprintMaxSpeedModifier;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _sprintAccelerationModifier;
        [SerializeField] private float _sprintHeatCostPerSecond;

        private Movable _movable;
        private bool SprintSircumstances 
            => Lazy.Controls.IsSprintHolded 
               && Lazy.Controls.MoveVector.magnitude > 0
               && Lazy.Resources.Heat.Value > 0;
        private Movable Movable => _movable ??= GetComponent<Movable>();
        
        public bool IsSprinting => Lazy.StateMachine.IsCurrentStateOneOf(StateMachine.Sprint);
        public Vector3 Velocity { get; private set; }

        private void FixedUpdate()
        {
            Lazy.Vitals.Immune = IsSprinting;
            
            if (SprintSircumstances)
            {
                Lazy.StateMachine.TryEnterState(StateMachine.Sprint);
            }
            else
            {
                Lazy.StateMachine.TryExitState(StateMachine.Sprint);
            }

            if (IsSprinting)
                Lazy.Resources.WasteHeat(_sprintHeatCostPerSecond * Time.fixedDeltaTime);

            if (Lazy.StateMachine.IsCurrentStateNoneOf(StateMachine.Performing))
            {
                Move(Lazy.Controls.MoveVector);
            }
            else
            {
                Movable.Velocity = Velocity = Vector3.zero;
            }
        }

        private void Move(Vector2 screenInput)
        {
            float maxSpeed = IsSprinting ? (_maxSpeed * _sprintMaxSpeedModifier) : _maxSpeed;
            float acceleration = IsSprinting ? (_acceleration * _sprintAccelerationModifier) : _acceleration;
            Vector3 targetVelocity = Lazy.CameraView.ScreenToPerspective(screenInput) * maxSpeed;
            Velocity = Vector3.MoveTowards(Velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            Movable.Velocity = Velocity;
        }
    }
}