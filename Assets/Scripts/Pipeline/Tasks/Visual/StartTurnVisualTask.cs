using Entities;
using Entities.Components;
using UI;

namespace Pipeline.Tasks.Visual
{
    public class StartTurnVisualTask : EventTask
    {
        private readonly IEntity _entityToActivate;
        private readonly IEntity _entityToDeactivate;

        public StartTurnVisualTask(IEntity entityToActivate, IEntity entityToDeactivate)
        {
            _entityToActivate = entityToActivate;
            _entityToDeactivate = entityToDeactivate;
        }

        protected override void OnRun()
        {
            _entityToActivate.Get<HeroPresentationComponent>().View.SetActive(true);
            
            if (_entityToDeactivate is not null)
                _entityToDeactivate.Get<HeroPresentationComponent>().View.SetActive(false);
            
            Finish();
        }
    }
}