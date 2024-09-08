using UnityEngine;

namespace Entities
{
    public abstract class SOEntity : ScriptableObject
    {
        private readonly BaseEntity _baseEntity = new();

        public void Add(object component) => _baseEntity.Add(component);
        
        public void RemoveAll() => _baseEntity.RemoveAll();
        
        public bool TryGet<T>(out T result) => _baseEntity.TryGet(out result);
    }
}