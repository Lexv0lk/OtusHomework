using Entities;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class StartTurnVisualHandler : VisualHandler<StartTurnEvent>
    {
        private IEntity _lastEntity;

        public StartTurnVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus, visualPipeline)
        {
            
        }

        protected override void OnHandleEvent(StartTurnEvent evt)
        {
            VisualPipeline.AddTask(new StartTurnVisualTask(evt.Entity, _lastEntity));
            _lastEntity = evt.Entity;
        }
    }
}