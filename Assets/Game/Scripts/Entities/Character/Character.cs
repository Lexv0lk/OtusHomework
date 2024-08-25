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

        [Get(LifeAPI.HEALTH)] 
        public IAtomicValueObservable<int> Health => CharacterCore.LifeComponent.HealthAmount;

        [Get(LifeAPI.IS_DEAD)] 
        public IAtomicValueObservable<bool> IsDead => CharacterCore.LifeComponent.IsDead;

        [Get(LifeAPI.DIE_EVENT)] 
        public AtomicEvent<AtomicEntity> DieEvent;
        
        [Get(LifeAPI.DIE_ANIMATION_EVENT)] 
        public AtomicEvent<AtomicEntity> DieAnimationEvent;

        [Get(TechAPI.RESET_ACTION)] 
        public IAtomicAction ResetAction => new AtomicAction(Reset);

        [SerializeField] protected CharacterCore CharacterCore;

        [SerializeField] protected CharacterAnimation CharacterAnimation;
        [SerializeField] protected CharacterVfx CharacterVfx;

        private void Awake()
        {
            CharacterCore.Compose();

            CharacterAnimation.Compose(CharacterCore, InvokeDieAnimationEvent);
            CharacterVfx.Compose(CharacterCore);
            
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
            OnReset();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void OnDestroy()
        {
            CharacterCore.LifeComponent.IsDead.Unsubscribe(OnDeadStateChanged);

            CharacterCore.Dispose();
            CharacterAnimation.Dispose();
            CharacterVfx.Dispose();
            
            OnDispose();
        }

        protected virtual void OnAwake() {}
        
        protected virtual void OnUpdate() {}
        
        protected virtual void OnDispose() {}
        
        protected virtual void OnReset() {}

        private void OnDeadStateChanged(bool isDead)
        {
            if (isDead)
                DieEvent.Invoke(this);
        }

        private void InvokeDieAnimationEvent()
        {
            DieAnimationEvent.Invoke(this);
        }
    }
}