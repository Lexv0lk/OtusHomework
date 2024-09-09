using Pipeline.Visual;

namespace EventBus.Handlers
{
    public abstract class VisualHandler<TEvent> : BaseHandler<TEvent>
    {
        protected readonly VisualPipeline VisualPipeline;

        protected VisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            VisualPipeline = visualPipeline;
        }
    }
}