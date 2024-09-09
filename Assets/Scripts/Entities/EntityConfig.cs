using UnityEngine;

namespace Entities
{
    public abstract class EntityConfig : ScriptableObject
    {
        private readonly BaseEntity _baseEntity = new();

        protected void Add(object component) => _baseEntity.Add(component);

        protected void RemoveAll() => _baseEntity.RemoveAll();

        public IEntity GetEntityInstance()
        {
            _baseEntity.RemoveAll();
            SetupComponents();
            
            var entity = new BaseEntity();
            
            foreach (var component in _baseEntity.GetAll())
                entity.Add(component);

            return entity;
        }

        protected virtual void SetupComponents() {}
    }
}