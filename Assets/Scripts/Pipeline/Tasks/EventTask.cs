using System;

namespace Pipeline.Tasks
{
    public abstract class EventTask
    {
        private Action _endCallback;

        public void Run(Action endCallback)
        {
            _endCallback = endCallback;
            OnRun();
        }

        public void Finish()
        {
            OnFinish();
            _endCallback?.Invoke();
        }

        protected virtual void OnRun() {}
        protected virtual void OnFinish() {}
    }
}