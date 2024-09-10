using Entities.Components;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class DestroyVisualHandler : VisualHandler<DestroyEvent>
    {
        private readonly AudioPlayer _audioPlayer;

        public DestroyVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, AudioPlayer audioPlayer) : base(eventBus, visualPipeline)
        {
            _audioPlayer = audioPlayer;
        }

        protected override void OnHandleEvent(DestroyEvent evt)
        {
            if (evt.Entity.TryGet<SoundsComponent>(out var sounds))
                VisualPipeline.AddTask(new AudioTask(sounds.GetRandomDeathClip(), _audioPlayer));
            
            if (evt.Entity.TryGet(out HeroPresentationComponent heroPresentationComponent))
                VisualPipeline.AddTask(new DestroyVisualTask(heroPresentationComponent.View));
        }
    }
}