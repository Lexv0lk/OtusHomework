using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class MoveComponent : ConditionalComponent
    {
        public AtomicVariable<Vector3> Direction;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed;

        public override void Compose()
        {
            
        }

        public override void Update(float deltaTime)
        {
            if (Condition.IsTrue())
                _root.position += Direction.Value * _speed * Time.deltaTime;
        }
    }
}