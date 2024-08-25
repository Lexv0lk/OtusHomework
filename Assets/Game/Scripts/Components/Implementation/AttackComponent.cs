using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class AttackComponent : ConditionalComponent
    {
        public AtomicEvent AttackRequest;
        public AtomicEvent AttackAction;
        public AtomicEvent AttackEvent;
        public AtomicEvent AttackEndEvent;

        public AtomicVariable<bool> IsInAttack;
        
        [SerializeField] private int _damage;
        [SerializeField] private float _hitReloadTime;

        private IAtomicAction<int> _targetTakeDamageAction;
        private IAtomicValue<bool> _isTargetReached;
        
        private ReloadingHitMechanic _reloadingHitMechanic;

        public void Compose(IAtomicValue<bool> isTargetReached, IAtomicAction<int> targetTakeDamageAction)
        {
            _targetTakeDamageAction = targetTakeDamageAction;
            _isTargetReached = isTargetReached;
            
            _reloadingHitMechanic = new ReloadingHitMechanic(isTargetReached,
                AttackRequest, _hitReloadTime);
            
            AttackAction.Subscribe(AttackTarget);
            AttackRequest.Subscribe(ChangeToAttackState);
            AttackEndEvent.Subscribe(ChangeToNonAttackState);
        }

        public void Dispose()
        {
            AttackAction.Unsubscribe(AttackTarget);
            AttackRequest.Unsubscribe(ChangeToAttackState);
            AttackEndEvent.Unsubscribe(ChangeToNonAttackState);
        }

        public void Reset()
        {
            IsInAttack.Value = false;
        }

        private void AttackTarget()
        {
            if (_isTargetReached.Value)
                _targetTakeDamageAction.Invoke(_damage);
            
            AttackEvent.Invoke();
        }

        private void ChangeToAttackState()
        {
            IsInAttack.Value = true;
        }

        private void ChangeToNonAttackState()
        {
            IsInAttack.Value = false;
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new[] { _reloadingHitMechanic };
        }
    }
}