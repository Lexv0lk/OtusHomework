using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite.Entities
{
    public sealed class EntityManager
    {
        private EcsWorld world;

        private readonly Dictionary<int, Entity> entities = new();
        private readonly PoolService _poolService;

        public event Action<Entity> Destroying;

        public EntityManager(PoolService poolService = null)
        {
            _poolService = poolService;
        }
        
        public void Initialize(EcsWorld world)
        {
            Entity[] entities = GameObject.FindObjectsOfType<Entity>();
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                Entity entity = entities[i];
                entity.Initialize(world);
                this.entities.Add(entity.Id, entity);
            }
            
            this.world = world;
        }

        public Entity Create(Entity prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            Entity entity;

            if (_poolService != null && _poolService.TryGet(prefab, out entity))
            {
                entity.transform.position = position;
                entity.transform.rotation = rotation;
                
                if (parent != null)
                    entity.transform.parent = parent;
            }
            else
            {
                entity = GameObject.Instantiate(prefab, position, rotation, parent);
            }
            
            entity.Initialize(this.world);
            this.entities.Add(entity.Id, entity);
            return entity;
        }

        public void Destroy(int id)
        {
            if (this.entities.Remove(id, out Entity entity))
            {
                entity.Dispose();
                Destroying?.Invoke(entity);
                
                if (_poolService == null || _poolService.TryRelease(entity) == false)
                    GameObject.Destroy(entity.gameObject);
            }
        }

        public Entity Get(int id)
        {
            return this.entities[id];
        }
    }
}