using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class HealVisualHandler : VisualHandler<HealEvent>
    {
        public HealVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus, visualPipeline)
        {
        }

        protected override void OnHandleEvent(HealEvent evt)
        {
            VisualPipeline.AddTask(new HealVisualTask(evt.Target, evt.Amount));
        }
    }
}