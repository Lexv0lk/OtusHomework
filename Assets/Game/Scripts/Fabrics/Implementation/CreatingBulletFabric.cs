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
        
        public IAtomicEntity GetBullet()
        {
            return GameObject.Instantiate(_config.BulletPrefab);
        }
    }
}