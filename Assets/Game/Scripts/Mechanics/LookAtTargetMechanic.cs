using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class LookAtTargetMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicValue<Vector3> _rootPosition;
        private readonly IAtomicVariable<Vector3> _rootForwardDirection;

        public LookAtTargetMechanic(IAtomicValue<Vector3> targetPosition, IAtomicValue<Vector3> rootPosition,
            IAtomicVariable<Vector3> rootForwardDirection)
        {
            _targetPosition = targetPosition;
            _rootPosition = rootPosition;
            _rootForwardDirection = rootForwardDirection;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _rootForwardDirection.Value = _targetPosition.Value - _rootPosition.Value;
        }
    }
}