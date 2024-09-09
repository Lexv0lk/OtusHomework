using Entities;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class StartTurnVisualHandler : BaseHandler<StartTurnEvent>
    {
        private readonly VisualPipeline _visualPipeline;

        private IEntity _lastEntity;

        public StartTurnVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void OnHandleEvent(StartTurnEvent evt)
        {
            _visualPipeline.AddTask(new StartTurnVisualTask(evt.Entity, _lastEntity));
            _lastEntity = evt.Entity;
        }
    }
}