using System;
using System.Collections.Generic;

namespace DI.Contexts
{
    public class ServicesContext
    {
        private readonly Dictionary<Type, object> _services = new();

        public void RegisterService<T>(T service)
        {
            Type serviceType = service.GetType();
            _services[serviceType] = service;
        }

        public T GetService<T>()
        {
            return (T)_services[typeof(T)];
        }

        public bool TryRemoveService<T>()
        {
            return _services.Remove(typeof(T));
        }
    }
}