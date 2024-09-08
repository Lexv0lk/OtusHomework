using Entities;

namespace EventBus.Events
{
    public struct AttackEvent
    {
        public IEntity Source;
        public IEntity Target;
        
        public AttackEvent(IEntity source, IEntity target)
        {
            Source = source;
            Target = target;
        }
    }
}