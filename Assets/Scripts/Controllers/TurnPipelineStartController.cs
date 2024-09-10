using Pipeline;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class TurnPipelineStartController : IInitializable
    {
        private readonly TurnPipeline _turnPipeline;

        public TurnPipelineStartController(TurnPipeline turnPipeline)
        {
            _turnPipeline = turnPipeline;
        }
        
        public void Initialize()
        {
            _turnPipeline.OnFinished += OnFinished;
            _turnPipeline.RunNextTask();
        }

        public void StopPipeline()
        {
            _turnPipeline.OnFinished -= OnFinished;
            _turnPipeline.Reset();
        }

        private void OnFinished()
        {
            _turnPipeline.Reset();
            _turnPipeline.RunNextTask();
        }
    }
}