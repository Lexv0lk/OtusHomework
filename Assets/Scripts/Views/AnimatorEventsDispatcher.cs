using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Views
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorEventsDispatcher : MonoBehaviour
    {
        private readonly Dictionary<string, List<Action>> _handlers = new();

        public void SubscribeOnEvent(string name, Action handler)
        {
            if (_handlers.TryGetValue(name, out List<Action> actions))
                actions.Add(handler);
            else
                _handlers[name] = new List<Action>() { handler };
        }

        public void UnsubcsribeFromEvent(string name, Action handler)
        {
            if (_handlers.TryGetValue(name, out List<Action> actions))
                actions.Remove(handler);
        }

        public void HandleAnimationEvent(string name)
        {
            if (_handlers.TryGetValue(name, out List<Action> actions))
                foreach (var action in actions)
                    action?.Invoke();
        }
    }
}