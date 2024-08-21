using System;
using Game.Scripts.Utilities;

namespace Game.Scripts.Components
{
    [Serializable]
    public abstract class ConditionalComponent : Component
    {
        protected readonly CompositeCondition Condition = new();

        public void AppendCondition(Func<bool> condition)
        {
            Condition.AddCondition(condition);
        }
    }
}