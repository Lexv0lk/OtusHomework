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

        public override void Compose()
        {
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
    }
}