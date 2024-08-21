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

        [Get(PhysicsAPI.COLLIDE_EVENT)] 
        public AtomicEvent<IAtomicEntity> Collided;

        [Get(TransformAPI.GAME_OBJECT)] 
        public IAtomicValue<GameObject> GameObject => new AtomicFunction<GameObject>(GetGameObject);

        [SerializeField] private RigidbodyMoveComponent _rigidbodyMoveComponent;
        [SerializeField] private int _damage = 1;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity atomicEntity))
                if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE, out var action))
                    action.Invoke(_damage);
            
            Collided.Invoke(this);
        }

        private GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}