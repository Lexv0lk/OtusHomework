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

        public void RemoveAll()
        {
            _components.Clear();
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