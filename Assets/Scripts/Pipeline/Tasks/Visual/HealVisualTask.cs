using Entities;
using Entities.Components;

namespace Pipeline.Tasks.Visual
{
    public class HealVisualTask : EventTask
    {
        private readonly IEntity _targetEntity;
        private readonly int _heal;

        public HealVisualTask(IEntity targetEntity, int heal)
        {
            _targetEntity = targetEntity;
            _heal = heal;
        }

        protected override void OnRun()
        {
            var stats = _targetEntity.Get<StatsComponent>();
            var view = _targetEntity.Get<HeroPresentationComponent>().View;
            
            view.SetStats($"{stats.Attack} / {stats.Health}");
            Finish();
        }
    }
}