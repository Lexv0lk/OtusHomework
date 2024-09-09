using Entities;

namespace EventBus.Events
{
    public struct TakeDamageEvent
    {
        public IEntity Target;
        public IEntity Source;
        public int Damage;
        
        public TakeDamageEvent(IEntity target, IEntity source, int damage)
        {
            Target = target;
            Source = source;
            Damage = damage;
        }
    }
}