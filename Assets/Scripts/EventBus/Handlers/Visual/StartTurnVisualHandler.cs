using Entities;
using Entities.Components;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class StartTurnVisualHandler : VisualHandler<StartTurnEvent>
    {
        private readonly AudioPlayer _audioPlayer;
        private IEntity _lastEntity;

        public StartTurnVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, AudioPlayer audioPlayer) : base(eventBus, visualPipeline)
        {
            _audioPlayer = audioPlayer;
        }

        protected override void OnHandleEvent(StartTurnEvent evt)
        {
            VisualPipeline.AddTask(new StartTurnVisualTask(evt.Entity, _lastEntity));
            
            if (evt.Entity.TryGet<SoundsComponent>(out var sounds))
                VisualPipeline.AddTask(new AudioTask(sounds.GetRandomStartTurnClip(), _audioPlayer));
            
            _lastEntity = evt.Entity;
        }
    }
}