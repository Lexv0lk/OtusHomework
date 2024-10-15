using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite.Entities
{
    public class EntitiesPool : MonoBehaviour
    {
        private readonly Queue<Entity> _availableEntites = new();
        
        private Entity _prefab;
        
        public void Initialize(Entity prefab, int count)
        {
            _prefab = prefab;
            
            for (int i = 0; i < count; i++)
                AddEntity();
        }

        public Entity Get()
        {
            if (_availableEntites.Count == 0)
                AddEntity();
            
            var entity = _availableEntites.Dequeue();
            entity.gameObject.SetActive(true);
            return entity;
        }

        public void Release(Entity entity)
        {
            entity.gameObject.SetActive(false);
            entity.transform.parent = transform;
            _availableEntites.Enqueue(entity);
        }
        
        private void AddEntity()
        {
            var instance = Instantiate(_prefab, transform);
            instance.gameObject.SetActive(false);
            _availableEntites.Enqueue(instance);
        }
    }
}