using Entities;

namespace EventBus.Events
{
    public struct SpecialAbilityEvent : IEvent
    {
        public IEntity Source;
        
        public SpecialAbilityEvent(IEntity source)
        {
            Source = source;
        }
    }
}