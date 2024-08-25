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
        public AtomicVariable<int> HealthAmount;

        private int _startHealthAmount;

        public override void Compose()
        {
            _startHealthAmount = HealthAmount.Value;
            TakeDamageAction.Subscribe(TakeDamage);
        }

        private void TakeDamage(int value)
        {
            if (IsDead.Value)
                return;

            int lastHealthAmount = HealthAmount.Value;
            HealthAmount.Value = Mathf.Max(0, HealthAmount.Value - value);

            if (HealthAmount.Value == 0)
                IsDead.Value = true;
            
            TakeDamageEvent?.Invoke(lastHealthAmount - HealthAmount.Value);
        }

        public void Reset()
        {
            HealthAmount.Value = _startHealthAmount;
            IsDead.Value = false;
        }
    }
}