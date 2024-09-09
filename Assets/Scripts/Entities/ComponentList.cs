using System.Collections.Generic;

namespace Entities
{
    public class ComponentList
    {
        private readonly List<object> _components = new();
        
        public void Add(object element)
        {
            _components.Add(element);
        }

        public void RemoveAll()
        {
            _components.Clear();
        }

        public IReadOnlyList<object> GetAll()
        {
            return _components;
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