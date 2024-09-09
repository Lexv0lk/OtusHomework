using Entities.Components;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class DestroyVisualHandler : VisualHandler<DestroyEvent>
    {
        public DestroyVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus, visualPipeline)
        {
        }

        protected override void OnHandleEvent(DestroyEvent evt)
        {
            if (evt.Entity.TryGet(out HeroPresentationComponent heroPresentationComponent))
                VisualPipeline.AddTask(new DestroyVisualTask(heroPresentationComponent.View));
        }
    }
}