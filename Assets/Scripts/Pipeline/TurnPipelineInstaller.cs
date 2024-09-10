using Pipeline.Tasks.Logic;
using Pipeline.Tasks.Visual;
using UnityEngine;
using Zenject;

namespace Pipeline
{
    public class TurnPipelineInstaller : IInitializable
    {
        private readonly TurnPipeline _turnPipeline;
        private readonly DiContainer _container;

        public TurnPipelineInstaller(TurnPipeline turnPipeline, DiContainer container)
        {
            _turnPipeline = turnPipeline;
            _container = container;
        }
        
        public void Initialize()
        {
            var startVisualPipelineTask = _container.Instantiate<StartVisualPipelineTask>();
            
            _turnPipeline.AddTask(_container.Instantiate<StartTurnTask>());
            _turnPipeline.AddTask(startVisualPipelineTask);
            _turnPipeline.AddTask(_container.Instantiate<PlayerInputTask>());
            _turnPipeline.AddTask(_container.Instantiate<ProceedTurnTask>());
            _turnPipeline.AddTask(startVisualPipelineTask);
            _turnPipeline.AddTask(_container.Instantiate<EndTurnTask>());
            _turnPipeline.AddTask(startVisualPipelineTask);
        }
    }
}