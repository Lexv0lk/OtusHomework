using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class RotateComponent : ConditionalComponent
    {
        public AtomicVariable<Vector3> ForwardDirection;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed;

        private Quaternion _cachedTargetRotation;

        public override void Compose()
        {
            ForwardDirection.Value = _root.forward;
        }

        public override void Update(float deltaTime)
        {
            if (Condition.IsTrue() == false)
                return;
            
            if (_root.forward == ForwardDirection.Value)
                return;

            _cachedTargetRotation = Quaternion.LookRotation(ForwardDirection.Value.normalized, Vector3.up);
            _root.rotation = Quaternion.Lerp(_root.rotation, _cachedTargetRotation, _speed * Time.deltaTime);
        }
    }
}