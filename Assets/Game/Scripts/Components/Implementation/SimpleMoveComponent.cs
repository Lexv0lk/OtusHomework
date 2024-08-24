using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class SimpleMoveComponent : ConditionalComponent
    {
        public AtomicVariable<Vector3> Direction;
        public AtomicVariable<bool> IsMoving;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed;

        public override void Compose()
        {
            Direction.Subscribe(OnDirectionChanged);
        }

        public override void Update(float deltaTime)
        {
            if (Condition.IsTrue())
                _root.position += Direction.Value * _speed * Time.deltaTime;
        }

        public override void Dispose()
        {
            Direction.Unsubscribe(OnDirectionChanged);
        }

        private void OnDirectionChanged(Vector3 newDirection)
        {
            IsMoving.Value = newDirection != Vector3.zero;
        }
    }
}