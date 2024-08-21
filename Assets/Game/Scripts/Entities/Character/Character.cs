using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities
{
    public abstract class Character : AtomicObject
    {
        [Get(MoveAPI.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => CharacterCore.MoveComponent.Direction;
        
        [Get(TransformAPI.FORWARD_DIRECTION)]
        public IAtomicVariable<Vector3> ForwardDirection => CharacterCore.RotateComponent.ForwardDirection;

        [Get(TransformAPI.POSITION)] 
        public IAtomicValue<Vector3> Position => new AtomicFunction<Vector3>(() => transform.position);

        [Get(LifeAPI.TAKE_DAMAGE)]
        public IAtomicAction<int> TakeDamageAction => CharacterCore.LifeComponent.TakeDamageAction;

        [Get(LifeAPI.IS_DEAD)] 
        public IAtomicObservable<bool> IsDead => CharacterCore.LifeComponent.IsDead;

        [SerializeField] protected CharacterCore CharacterCore;

        private void Awake()
        {
            CharacterCore.Compose();
            OnAwake();
        }

        private void Update()
        {
            CharacterCore.Update(Time.deltaTime);
            OnUpdate();
        }

        private void OnDestroy()
        {
            CharacterCore.Dispose();
            OnDispose();
        }

        protected virtual void OnAwake() {}
        
        protected virtual void OnUpdate() {}
        
        protected virtual void OnDispose() {}
    }
}