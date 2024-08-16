using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities.Character
{
    public class Character : AtomicEntity
    {
        [Get(MoveAPI.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _core.MoveComponent.Direction;
        
        [Get(TransformAPI.FORWARD_DIRECTION)]
        public IAtomicVariable<Vector3> ForwardDirection => _core.RotateComponent.ForwardDirection;

        [Get(TransformAPI.POSITION)] 
        public IAtomicValue<Vector3> Position => new AtomicFunction<Vector3>(() => transform.position);
        
        [SerializeField] private CharacterCore _core;

        private void Awake()
        {
            _core.Compose();
        }

        private void Update()
        {
            _core.Update(Time.deltaTime);
        }
    }
}