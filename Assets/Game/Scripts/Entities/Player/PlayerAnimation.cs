using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        [SerializeField] private float _animationValueChangeSpeed = 10;
        
        [Header("Animation Keys")] 
        [SerializeField] private string _deadBoolean = "IsDead";
        [SerializeField] private string _moveBoolean = "IsMoving";
        [SerializeField] private string _moveXFloat = "MoveX";
        [SerializeField] private string _moveYFloat = "MoveZ";
        [SerializeField] private string _attackTrigger = "Shoot";
        [SerializeField] private string _takeDamageTrigger = "TakeDamage";
        
        [Header("Animation Events")] 
        [SerializeField] private string _deadEvent = "Died";
        [SerializeField] private string _attackEvent = "Shooted";

        private AtomicEvent _takeDamageEvent = new();
        private PlayerCore _core;
        private Action _dieAnimationAction;
        
        private BoolAnimationMechanics _moveAnimationMechanics;
        private BoolAnimationMechanics _deadAnimationMechanics;
        private Vector2AnimationMechanics _vector2AnimationMechanics;
        private InverseMoveDirectionMechanics _inverseMoveDirectionMechanics;
        private AttackAnimationMechanics _attackAnimationMechanics;
        private TriggerAnimationMechanics _takeDamageAnimationMechanics;

        public void Compose(PlayerCore core, Action dieAnimationAction, Transform transform)
        {
            _core = core;
            _dieAnimationAction = dieAnimationAction;
            
            _moveAnimationMechanics = new BoolAnimationMechanics(core.MoveComponent.IsMoving,
                _animator, Animator.StringToHash(_moveBoolean));

            _deadAnimationMechanics = new BoolAnimationMechanics(core.LifeComponent.IsDead, _animator,
                Animator.StringToHash(_deadBoolean));
            
            _inverseMoveDirectionMechanics =
                new InverseMoveDirectionMechanics(transform, _core.MoveComponent.Direction);
            
            _vector2AnimationMechanics = new Vector2AnimationMechanics(_inverseMoveDirectionMechanics.ConvertedDirection,
                _animator, Animator.StringToHash(_moveXFloat), Animator.StringToHash(_moveYFloat), _animationValueChangeSpeed);

            _attackAnimationMechanics = new AttackAnimationMechanics(_animator,
                _animatorDispatcher, _attackTrigger, _attackEvent,
                core.ShootComponent.ShootRequest, core.ShootComponent.ShootAction,
                core.ShootComponent.CanShoot);

            _takeDamageAnimationMechanics = new TriggerAnimationMechanics(_takeDamageEvent,
                _animator, Animator.StringToHash(_takeDamageTrigger));
            
            _core.LifeComponent.TakeDamageEvent.Subscribe(OnCharacterTakeDamageEvent);
            _animatorDispatcher.SubscribeOnEvent(_deadEvent, _dieAnimationAction);
        }

        public void Dispose()
        {
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(OnCharacterTakeDamageEvent);
            _animatorDispatcher.UnsubscribeOnEvent(_deadEvent, _dieAnimationAction);
        }

        private void OnCharacterTakeDamageEvent(int _)
        {
            _takeDamageEvent.Invoke();
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new IAtomicLogic[] { _vector2AnimationMechanics, _inverseMoveDirectionMechanics,
                _attackAnimationMechanics, _takeDamageAnimationMechanics, _moveAnimationMechanics,
                _deadAnimationMechanics };
        }
    }
}