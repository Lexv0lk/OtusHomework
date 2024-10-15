using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite.Entities
{
    public class PoolService : MonoBehaviour
    {
        [SerializeField] private EntityPoolSettings[] _pools;

        private readonly Dictionary<Entity, EntitiesPool> _activeEntities = new();

        private void Awake()
        {
            foreach (var poolSettings in _pools)
                poolSettings.Pool.Initialize(poolSettings.Prefab, poolSettings.StartCount);
        }

        public bool TryGet(Entity prefab, out Entity instance)
        {
            instance = default;
            
            foreach (var poolSettings in _pools)
            {
                if (poolSettings.Prefab == prefab)
                {
                    instance = poolSettings.Pool.Get();
                    _activeEntities[instance] = poolSettings.Pool;
                    return true;
                }
            }

            return false;
        }

        public bool TryRelease(Entity entity)
        {
            if (_activeEntities.ContainsKey(entity))
            {
                _activeEntities[entity].Release(entity);
                _activeEntities.Remove(entity);
                return true;
            }

            return false;
        }
        
        [Serializable]
        private struct EntityPoolSettings
        {
            public Entity Prefab;
            public int StartCount;
            public EntitiesPool Pool;
        }
    }
}