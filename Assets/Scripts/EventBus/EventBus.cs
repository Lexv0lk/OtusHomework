using System;
using System.Collections.Generic;

namespace EventBus
{
    public class EventBus
    {
        private readonly Dictionary<Type, IEventHandlerCollection> _handlers = new();
        
        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var key = typeof(TEvent);

            if (_handlers.ContainsKey(key) == false)
                _handlers[key] = new EventHandlerCollection<TEvent>();
            
            _handlers[key].Subscribe(handler);
        }
        
        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            var key = typeof(TEvent);
            
            if (_handlers.TryGetValue(key, out var handlers))
                handlers.Unsubscribe(handler);
        }
        
        public void RaiseEvent<TEvent>(TEvent evt)
        {
            var key = evt.GetType();

            if (_handlers.TryGetValue(key, out var handlers))
                handlers.RaiseEvent(evt);
        }
    }
}