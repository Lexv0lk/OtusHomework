using Entities.Components;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class AttackVisualHandler : VisualHandler<AttackEvent>
    {
        public AttackVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus, visualPipeline)
        {
        }

        protected override void OnHandleEvent(AttackEvent evt)
        {
            VisualPipeline.AddTask(new AttackVisualTask(evt.Source.Get<HeroPresentationComponent>().View,
                evt.Target.Get<HeroPresentationComponent>().View));
        }
    }
}