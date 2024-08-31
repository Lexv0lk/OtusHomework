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

        private List<IAtomicLogic> _mechanics = new();

        public void Compose(IAtomicValue<bool> isTargetReached, IAtomicAction<int> targetTakeDamageAction)
        {
            _targetTakeDamageAction = targetTakeDamageAction;
            _isTargetReached = isTargetReached;
            
            ReloadingHitMechanic hitMechanic = new ReloadingHitMechanic(isTargetReached,
                AttackRequest, _hitReloadTime);

            ConditionalStateAttackMechanic attackMechanic = new ConditionalStateAttackMechanic(AttackRequest,
                AttackAction, AttackEndEvent, _isTargetReached, _targetTakeDamageAction, AttackEvent, IsInAttack,
                _damage);
            
            _mechanics.Add(hitMechanic);
            _mechanics.Add(attackMechanic);
        }

        public IEnumerable<IAtomicLogic> GetMechanics() => _mechanics;

        public void Reset()
        {
            IsInAttack.Value = false;
        }
    }
}