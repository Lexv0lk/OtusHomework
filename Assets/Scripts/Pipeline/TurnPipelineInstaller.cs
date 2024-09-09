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
            _turnPipeline.AddTask(_container.Instantiate<StartTurnTask>());
            _turnPipeline.AddTask(_container.Instantiate<StartVisualPipelineTask>());
            _turnPipeline.AddTask(_container.Instantiate<PlayerInputTask>());
        }
    }
}