using Cysharp.Threading.Tasks;
using UI;

namespace Pipeline.Tasks.Visual
{
    public class AttackVisualTask : EventTask
    {
        private readonly HeroView _sourceView;
        private readonly HeroView _targetView;

        public AttackVisualTask(HeroView sourceView, HeroView targetView)
        {
            _sourceView = sourceView;
            _targetView = targetView;
        }

        protected override void OnRun()
        {
            AnimateAttack().Forget();
        }

        private async UniTaskVoid AnimateAttack()
        {
            await _sourceView.AnimateAttack(_targetView);
            Finish();
        }
    }
}