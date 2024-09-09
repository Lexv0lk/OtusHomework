using System;
using System.Collections.Generic;

namespace Entities
{
    public class BaseEntity : IEntity
    {
        private readonly List<object> _components = new();
        
        public void Add(object element)
        {
            _components.Add(element);
        }

        public void Remove<T>()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T)
                {
                    _components.RemoveAt(i);
                    return;
                }
            }

            throw new IndexOutOfRangeException();
        }

        public void RemoveAll()
        {
            _components.Clear();
        }

        public IReadOnlyList<object> GetAll()
        {
            return _components;
        }

        public void AddRange(params object[] components)
        {
            foreach (var component in components)
                Add(component);
        }

        public T Get<T>()
        {
            foreach (object component in _components)
            {
                if (component is T concreteComponent)
                    return concreteComponent;
            }

            return default;
        }
        
        public void Set<T>(T element)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T)
                {
                    _components[i] = element;
                    return;
                }
            }
        }
        
        public bool TryGet<T>(out T element)
        {
            foreach (object component in _components)
            {
                if (component is T concreteElement)
                {
                    element = concreteElement;
                    return true;
                }
            }

            element = default;
            return false;
        }
    }
}