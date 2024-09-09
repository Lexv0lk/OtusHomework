using Entities;

namespace EventBus.Events
{
    public struct DestroyEvent
    {
        public IEntity Entity;

        public DestroyEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}