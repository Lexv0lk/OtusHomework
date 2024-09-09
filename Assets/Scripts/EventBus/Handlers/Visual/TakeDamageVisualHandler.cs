using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class TakeDamageVisualHandler : VisualHandler<TakeDamageEvent>
    {
        public TakeDamageVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus, visualPipeline)
        {
        }

        protected override void OnHandleEvent(TakeDamageEvent evt)
        {
            VisualPipeline.AddTask(new TakeDamageVisualTask(evt.Target, evt.Damage));
        }
    }
}