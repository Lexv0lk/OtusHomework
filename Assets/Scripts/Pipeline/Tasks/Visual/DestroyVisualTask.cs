using UI;

namespace Pipeline.Tasks.Visual
{
    public class DestroyVisualTask : EventTask
    {
        private readonly HeroView _view;

        public DestroyVisualTask(HeroView view)
        {
            _view = view;
        }

        protected override void OnRun()
        {
            _view.gameObject.SetActive(false);
            Finish();
        }
    }
}