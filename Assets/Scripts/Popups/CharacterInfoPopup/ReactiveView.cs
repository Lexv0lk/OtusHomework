using UniRx;
using UnityEngine;

namespace Popups.CharacterInfoPopup
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