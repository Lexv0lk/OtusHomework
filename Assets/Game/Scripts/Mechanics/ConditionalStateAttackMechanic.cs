using Atomic.Elements;
using Atomic.Objects;

namespace Game.Scripts.Mechanics
{
    public class ConditionalStateAttackMechanic : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicObservable _attackRequest;
        private readonly IAtomicObservable _attackAction;
        private readonly IAtomicObservable _attackEndEvent;
        private readonly IAtomicValue<bool> _canAttackTarget;
        private readonly IAtomicAction<int> _targetTakeDamageAction;
        private readonly IAtomicAction _attackEvent;
        private readonly IAtomicVariable<bool> _isInAttack;
        private readonly int _damage;

        public ConditionalStateAttackMechanic(IAtomicObservable attackRequest, IAtomicObservable attackAction,
            IAtomicObservable attackEndEvent, IAtomicValue<bool> canAttackTarget,
            IAtomicAction<int> targetTakeDamageAction, IAtomicAction attackEvent,
            IAtomicVariable<bool> isInAttack, int damage)
        {
            _attackRequest = attackRequest;
            _attackAction = attackAction;
            _attackEndEvent = attackEndEvent;
            _canAttackTarget = canAttackTarget;
            _targetTakeDamageAction = targetTakeDamageAction;
            _attackEvent = attackEvent;
            _isInAttack = isInAttack;
            _damage = damage;
        }

        public void Enable()
        {
            _attackAction.Subscribe(AttackTarget);
            _attackRequest.Subscribe(ChangeToAttackState);
            _attackEndEvent.Subscribe(ChangeToNonAttackState);
        }

        public void Disable()
        {
            _attackAction.Unsubscribe(AttackTarget);
            _attackRequest.Unsubscribe(ChangeToAttackState);
            _attackEndEvent.Unsubscribe(ChangeToNonAttackState);
        }
        
        private void AttackTarget()
        {
            if (_canAttackTarget.Value)
                _targetTakeDamageAction.Invoke(_damage);
            
            _attackEvent.Invoke();
        }

        private void ChangeToAttackState()
        {
            _isInAttack.Value = true;
        }

        private void ChangeToNonAttackState()
        {
            _isInAttack.Value = false;
        }
    }
}