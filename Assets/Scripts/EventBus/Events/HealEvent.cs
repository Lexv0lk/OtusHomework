using Entities;

namespace EventBus.Events
{
    public struct HealEvent
    {
        public IEntity Target;
        public int Amount;
        
        public HealEvent(IEntity target, int amount)
        {
            Target = target;
            Amount = amount;
        }
    }
}