using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Components;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public class Bullet : AtomicEntity
    {
        [Get(MoveAPI.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _rigidbodyMoveComponent.Direction;
        
        [Get(TransformAPI.POSITION)] 
        public AtomicVariable<Vector3> Position;
        
        [SerializeField] private RigidbodyMoveComponent _rigidbodyMoveComponent;

        private void Awake()
        {
            _rigidbodyMoveComponent.Compose();
            Position.Subscribe(ChangePosition);
        }

        private void Update()
        {
            _rigidbodyMoveComponent.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _rigidbodyMoveComponent.Dispose();
            Position.Unsubscribe(ChangePosition);
        }

        private void ChangePosition(Vector3 newPos)
        {
            transform.position = newPos;
        }
    }
}