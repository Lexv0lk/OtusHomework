using Entities.Components;
using EventBus.Events;
using Utils;

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
            targetStats.CurrentHealth += evt.Amount;
            evt.Target.Set(targetStats);
            
            EntityDebug.Log(evt.Target, $"healed {evt.Amount} hp");
        }
    }
}