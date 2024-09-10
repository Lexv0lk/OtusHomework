using Entities.Components;
using EventBus.Events;
using UnityEngine;

namespace EventBus.Handlers.Logic
{
    public class AttackEventHandler : BaseHandler<AttackEvent>
    {
        public AttackEventHandler(EventBus eventBus) : base(eventBus)
        {
        }

        protected override void OnHandleEvent(AttackEvent evt)
        {
            var attackerStats = evt.Source.Get<StatsComponent>();
            EventBus.RaiseEvent(new TakeDamageEvent(evt.Target, evt.Source, attackerStats.Attack));
        }
    }
}