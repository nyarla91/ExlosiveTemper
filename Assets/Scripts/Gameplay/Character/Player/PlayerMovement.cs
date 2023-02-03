using Extentions;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class PlayerMovement : LazyGetComponent<PlayerComposition>
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _sprintMaxSpeedModifier;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _sprintAccelerationModifier;
        [SerializeField] private float _sprintHeatCostPerSecond;

        private bool SprintSircumstances 
            => Lazy.Controls.IsSprintHolded 
               && Lazy.Controls.MoveVector.magnitude > 0
               && Lazy.Resources.Heat.Value > 0;
        
        public bool IsSprinting => Lazy.StateMachine.IsCurrentStateOneOf(StateMachine.Sprint);
        public Vector3 Velocity { get; private set; }
        
        [Inject] private Pause Pause { get; set; }

        private void FixedUpdate()
        {
            Lazy.Hitbox.Immune = IsSprinting;
            
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
                Lazy.Movable.VoluntaryVelocity = Velocity = Vector3.zero;
            }
        }

        private void Move(Vector2 screenInput)
        {
            if (Pause.IsPaused)
                return;
            float maxSpeed = IsSprinting ? (_maxSpeed * _sprintMaxSpeedModifier) : _maxSpeed;
            float acceleration = IsSprinting ? (_acceleration * _sprintAccelerationModifier) : _acceleration;
            Vector3 targetVelocity = Lazy.CameraView.ScreenToPerspective(screenInput) * maxSpeed;
            Velocity = Vector3.MoveTowards(Velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            Lazy.Movable.VoluntaryVelocity = Velocity;
        }
    }
}