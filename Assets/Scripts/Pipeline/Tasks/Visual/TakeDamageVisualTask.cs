using Entities;
using Entities.Components;

namespace Pipeline.Tasks.Visual
{
    public class TakeDamageVisualTask : EventTask
    {
        private readonly IEntity _targetEntity;
        private readonly int _damage;

        public TakeDamageVisualTask(IEntity targetEntity, int damage)
        {
            _targetEntity = targetEntity;
            _damage = damage;
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