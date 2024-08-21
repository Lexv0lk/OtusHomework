using System;
using Atomic.Objects;
using Game.Scripts.Configs.Fabrics;
using UnityEngine;

namespace Game.Scripts.Fabrics
{
    public class CreatingBulletFabric : IBulletFabric
    {
        private readonly BulletFabricConfig _config;

        public CreatingBulletFabric(BulletFabricConfig config)
        {
            _config = config;
        }

        public event Action<IAtomicEntity> CreatedBullet;
        
        public IAtomicEntity GetBullet()
        {
            IAtomicEntity bullet = GameObject.Instantiate(_config.BulletPrefab);
            CreatedBullet?.Invoke(bullet);
            return bullet;
        }
    }
}