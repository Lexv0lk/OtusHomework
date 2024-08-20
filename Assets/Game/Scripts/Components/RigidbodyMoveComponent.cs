using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class RigidbodyMoveComponent : ConditionalComponent
    {
        public AtomicVariable<Vector3> Direction;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        public override void Compose()
        {
            Direction.Subscribe(OnDirectionChanged);
        }

        public override void Dispose()
        {
            Direction.Unsubscribe(OnDirectionChanged);
        }

        private void OnDirectionChanged(Vector3 newDirection)
        {
            _rigidbody.velocity = Direction.Value.normalized * _speed;
        }
    }
}