using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyAnimation
    {
        [Header("Animation Keys")] 
        [SerializeField] private string _attackTrigger = "Attack";
        
        [Header("Animation Events")] 
        [SerializeField] private string _attackEvent = "Attacked";
        [SerializeField] private string _attackEndEvent = "AttackEnd";

        private EnemyCore _enemyCore;
        private CharacterAnimation _characterAnimation;
        private IAtomicValue<bool> _isAttackTargetDead;
        
        private AttackAnimationMechanics _attackAnimationMechanics;
        
        public void Compose(EnemyCore enemyCore, CharacterAnimation characterAnimation, IAtomicValue<bool> isAttackTargetDead)
        {
            _enemyCore = enemyCore;
            _characterAnimation = characterAnimation;
            _isAttackTargetDead = isAttackTargetDead;
            
            AtomicFunction<bool> canAttack = new AtomicFunction<bool>(CanAttack);
            
            _attackAnimationMechanics = new AttackAnimationMechanics(characterAnimation.Animator,
                characterAnimation.AnimatorDispatcher, _attackTrigger, _attackEvent,
                enemyCore.AttackComponent.AttackRequest, enemyCore.AttackComponent.AttackAction,
                canAttack);
            
            _characterAnimation.AnimatorDispatcher.SubscribeOnEvent(_attackEndEvent, OnAttackAnimationEnded);
        }

        public void Dispose()
        {
            _characterAnimation.AnimatorDispatcher.UnsubscribeOnEvent(_attackEndEvent, OnAttackAnimationEnded);
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new [] { _attackAnimationMechanics };
        }

        private void OnAttackAnimationEnded()
        {
            _enemyCore.AttackComponent.AttackEndEvent.Invoke();
        }

        private bool CanAttack()
        {
            return !_isAttackTargetDead.Value;
        }
    }
}