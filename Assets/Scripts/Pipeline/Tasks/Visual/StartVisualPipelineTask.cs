using Pipeline.Visual;

namespace Pipeline.Tasks.Visual
{
    public class StartVisualPipelineTask : EventTask
    {
        private readonly VisualPipeline _visualPipeline;

        public StartVisualPipelineTask(VisualPipeline visualPipeline)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void OnRun()
        {
            _visualPipeline.OnFinished += OnFinishedPipeline;
            _visualPipeline.RunNextTask();
        }

        protected override void OnFinish()
        {
            _visualPipeline.OnFinished -= OnFinishedPipeline;
        }

        private void OnFinishedPipeline()
        {
            _visualPipeline.ClearAll();
            Finish();
        }
    }
}