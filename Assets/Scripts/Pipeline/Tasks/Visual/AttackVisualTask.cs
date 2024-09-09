using Cysharp.Threading.Tasks;
using Entities;
using Entities.Components;

namespace Pipeline.Tasks.Visual
{
    public class AttackVisualTask : EventTask
    {
        private readonly IEntity _sourceEntity;
        private readonly IEntity _targetEntity;

        public AttackVisualTask(IEntity sourceEntity, IEntity targetEntity)
        {
            _sourceEntity = sourceEntity;
            _targetEntity = targetEntity;
        }

        protected override void OnRun()
        {
            AnimateAttack().Forget();
        }

        private async UniTaskVoid AnimateAttack()
        {
            var sourceView = _sourceEntity.Get<HeroPresentationComponent>().View;
            var targetView = _targetEntity.Get<HeroPresentationComponent>().View;
            
            await sourceView.AnimateAttack(targetView);
            
            Finish();
        }
    }
}