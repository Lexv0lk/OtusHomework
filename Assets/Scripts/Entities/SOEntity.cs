using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public abstract class SOEntity : ScriptableObject, IEntity
    {
        private readonly ComponentList _componentList = new();

        public void Add(object component) => _componentList.Add(component);
        
        public void RemoveAll() => _componentList.RemoveAll();
        
        public bool TryGet<T>(out T result) => _componentList.TryGet(out result);

        public T Get<T>() => _componentList.Get<T>();

        public IReadOnlyList<object> GetAll() => _componentList.GetAll();
    }
}