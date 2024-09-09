using Entities.Components;
using EventBus.Events;

namespace EventBus.Handlers.Logic
{
    public class HealEventHandler : BaseHandler<HealEvent>
    {
        public HealEventHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void OnHandleEvent(HealEvent evt)
        {
            var targetStats = evt.Target.Get<StatsComponent>();
            targetStats.Health += evt.Amount;
            evt.Target.Set(targetStats);
        }
    }
}