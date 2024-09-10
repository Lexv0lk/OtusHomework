using Entities;

namespace EventBus.Events
{
    public struct LowHealthEvent : IEvent
    {
        public IEntity Entity;
        
        public LowHealthEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}