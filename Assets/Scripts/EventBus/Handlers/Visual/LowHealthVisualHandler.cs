using Entities.Components;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class LowHealthVisualHandler : VisualHandler<LowHealthEvent>
    {
        private readonly AudioPlayer _audioPlayer;

        public LowHealthVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, AudioPlayer audioPlayer) : base(eventBus, visualPipeline)
        {
            _audioPlayer = audioPlayer;
        }

        protected override void OnHandleEvent(LowHealthEvent evt)
        {
            if (evt.Entity.TryGet<SoundsComponent>(out var sounds))
                VisualPipeline.AddTask(new AudioTask(sounds.GetRandomLowHealthClip(), _audioPlayer));
        }
    }
}