using System;
using System.Collections.Generic;
using UnityEngine;

namespace Popups.CharacterInfoPopup
{
    public abstract class ReactiveView : MonoBehaviour
    {
        protected readonly List<IDisposable> Subscriptions = new();

        private void OnDestroy()
        {
            DisposeSubscriptions();
        }

        protected void DisposeSubscriptions()
        {
            for (int i = 0; i < Subscriptions.Count; i++)
                Subscriptions[i].Dispose();
            
            Subscriptions.Clear();
        }
    }
}