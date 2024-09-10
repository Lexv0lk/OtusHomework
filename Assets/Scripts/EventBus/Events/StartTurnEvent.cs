using Entities;

namespace EventBus.Events
{
    public struct StartTurnEvent : IEvent
    {
        public IEntity Entity;

        public StartTurnEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}