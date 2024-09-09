using Entities;

namespace EventBus.Events
{
    public struct TurnAttackEvent
    {
        public IEntity Source;
        public IEntity Target;
        
        public TurnAttackEvent(IEntity source, IEntity target)
        {
            Source = source;
            Target = target;
        }
    }
}