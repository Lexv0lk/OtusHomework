using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerAnimation
    {
        [SerializeField] private float _animationValueChangeSpeed = 10;
        
        [Header("Animation Keys")] 
        [SerializeField] private string _moveXFloat = "MoveX";
        [SerializeField] private string _moveYFloat = "MoveZ";
        [SerializeField] private string _attackTrigger = "Shoot";
        [SerializeField] private string _takeDamageTrigger = "TakeDamage";
        
        [Header("Animation Events")] 
        [SerializeField] private string _attackEvent = "Shooted";

        private AtomicEvent _takeDamageEvent = new();
        private CharacterCore _characterCore;
        
        private Vector2AnimationMechanics _vector2AnimationMechanics;
        private InverseMoveDirectionMechanics _inverseMoveDirectionMechanics;
        private AttackAnimationMechanics _attackAnimationMechanics;
        private TriggerAnimationMechanics _takeDamageAnimationMechanics;

        public void Compose(CharacterCore characterCore, PlayerCore playerCore, CharacterAnimation characterAnimation, Transform transform)
        {
            _characterCore = characterCore;
            
            _inverseMoveDirectionMechanics =
                new InverseMoveDirectionMechanics(transform, characterCore.MoveComponent.Direction);
            
            _vector2AnimationMechanics = new Vector2AnimationMechanics(_inverseMoveDirectionMechanics.ConvertedDirection,
                characterAnimation.Animator, Animator.StringToHash(_moveXFloat), Animator.StringToHash(_moveYFloat), _animationValueChangeSpeed);

            _attackAnimationMechanics = new AttackAnimationMechanics(characterAnimation.Animator,
                characterAnimation.AnimatorDispatcher, _attackTrigger, _attackEvent,
                playerCore.ShootComponent.ShootRequest, playerCore.ShootComponent.ShootAction,
                playerCore.ShootComponent.CanShoot);

            _takeDamageAnimationMechanics = new TriggerAnimationMechanics(_takeDamageEvent,
                characterAnimation.Animator, Animator.StringToHash(_takeDamageTrigger));
            
            _characterCore.LifeComponent.TakeDamageEvent.Subscribe(OnCharacterTakeDamageEvent);
        }

        public void Dispose()
        {
            _characterCore.LifeComponent.TakeDamageEvent.Unsubscribe(OnCharacterTakeDamageEvent);
        }

        private void OnCharacterTakeDamageEvent(int _)
        {
            _takeDamageEvent.Invoke();
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new IAtomicLogic[] { _vector2AnimationMechanics, _inverseMoveDirectionMechanics, _attackAnimationMechanics, _takeDamageAnimationMechanics };
        }
    }
}