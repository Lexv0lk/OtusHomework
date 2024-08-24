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
        
        [Get(MoveAPI.FORWARD_DIRECTION)]
        public IAtomicVariable<Vector3> ForwardDirection => CharacterCore.RotateComponent.ForwardDirection;

        [Get(LifeAPI.TAKE_DAMAGE_ACTION)]
        public IAtomicAction<int> TakeDamageAction => CharacterCore.LifeComponent.TakeDamageAction;

        [Get(LifeAPI.IS_DEAD)] 
        public IAtomicObservable<bool> IsDead => CharacterCore.LifeComponent.IsDead;

        [Get(LifeAPI.DIE_EVENT)] 
        public AtomicEvent<AtomicEntity> DieEvent;

        [Get(TechAPI.RESET_ACTION)] 
        public IAtomicAction ResetAction => new AtomicAction(Reset);

        [SerializeField] protected CharacterCore CharacterCore;

        [SerializeField] protected CharacterAnimation CharacterAnimation;

        private void Awake()
        {
            CharacterCore.Compose();

            CharacterAnimation.Compose(CharacterCore);
            
            CharacterCore.LifeComponent.IsDead.Subscribe(OnDeadStateChanged);

            foreach (var mechanic in CharacterAnimation.GetMechanics())
                AddLogic(mechanic);
            
            OnAwake();
        }

        private void OnEnable()
        {
            Enable();
        }

        private void Update()
        {
            CharacterCore.Update(Time.deltaTime);
            OnUpdate();
            
            base.OnUpdate(Time.deltaTime);
        }

        public void Reset()
        {
            CharacterCore.LifeComponent.Reset();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void OnDestroy()
        {
            CharacterCore.LifeComponent.IsDead.Unsubscribe(OnDeadStateChanged);

            CharacterCore.Dispose();
            
            OnDispose();
        }

        protected virtual void OnAwake() {}
        
        protected virtual void OnUpdate() {}
        
        protected virtual void OnDispose() {}

        private void OnDeadStateChanged(bool isDead)
        {
            if (isDead)
                DieEvent.Invoke(this);
        }
    }
}