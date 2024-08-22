using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class LifeComponent : Component
    {
        public AtomicEvent<int> TakeDamageAction;
        public AtomicEvent<int> TakeDamageEvent;
        public AtomicVariable<bool> IsDead;

        [SerializeField] private int _healthAmount;

        private int _startHealthAmount;

        public override void Compose()
        {
            _startHealthAmount = _healthAmount;
            TakeDamageAction.Subscribe(TakeDamage);
        }

        private void TakeDamage(int value)
        {
            if (IsDead.Value)
                return;

            int lastHealthAmount = _healthAmount;
            _healthAmount = Mathf.Max(0, _healthAmount - value);

            if (_healthAmount == 0)
                IsDead.Value = true;
            
            TakeDamageEvent?.Invoke(lastHealthAmount - _healthAmount);
        }

        public void Reset()
        {
            _healthAmount = _startHealthAmount;
            IsDead.Value = false;
        }
    }
}