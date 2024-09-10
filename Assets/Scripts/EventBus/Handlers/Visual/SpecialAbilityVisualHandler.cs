using Entities.Components;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class SpecialAbilityVisualHandler : VisualHandler<SpecialAbilityEvent>
    {
        private readonly AudioPlayer _audioPlayer;

        public SpecialAbilityVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, AudioPlayer audioPlayer) : base(eventBus, visualPipeline)
        {
            _audioPlayer = audioPlayer;
        }

        protected override void OnHandleEvent(SpecialAbilityEvent evt)
        {
            if (evt.Source.TryGet<SoundsComponent>(out var sounds))
                VisualPipeline.AddTask(new AudioTask(sounds.GetRandomAbilityClip(), _audioPlayer));
        }
    }
}