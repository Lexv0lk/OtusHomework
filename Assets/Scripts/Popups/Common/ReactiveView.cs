using UniRx;
using UnityEngine;

namespace Popups.Common
{
    public abstract class ReactiveView : MonoBehaviour
    {
        protected CompositeDisposable Subscriptions = new();

        private void OnDestroy()
        {
            Subscriptions?.Dispose();
        }

        protected void DisposeSubscriptions()
        {
            Subscriptions?.Dispose();
            Subscriptions = new();
        }
    }
}