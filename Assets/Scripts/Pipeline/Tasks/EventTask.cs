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
            _endCallback?.Invoke();
            OnFinish();
        }

        protected virtual void OnRun() {}
        protected virtual void OnFinish() {}
    }
}