using Entities;
using Entities.Components;
using UI;

namespace Pipeline.Tasks.Visual
{
    public class TakeDamageVisualTask : EventTask
    {
        private readonly IEntity _targetEntity;
        private readonly HeroView _targetView;
        private readonly StatsComponent _newStats;

        public TakeDamageVisualTask(HeroView targetView, StatsComponent newStats)
        {
            _targetView = targetView;
            _newStats = newStats;
        }

        protected override void OnRun()
        {
            _targetView.SetStats($"{_newStats.Attack} / {_newStats.CurrentHealth}");
            Finish();
        }
    }
}