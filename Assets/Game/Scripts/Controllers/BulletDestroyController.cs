using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Fabrics;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class BulletDestroyController
    {
        private readonly IBulletFabric _bulletFabric;

        public BulletDestroyController(IBulletFabric bulletFabric)
        {
            _bulletFabric = bulletFabric;
            _bulletFabric.CreatedBullet += OnBulletCreated;
        }

        private void OnBulletCreated(IAtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<IAtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Subscribe(OnBulletCollided);
        }

        private void OnBulletCollided(IAtomicEntity bullet)
        {
            bullet.Get<IAtomicEvent<IAtomicEntity>>(PhysicsAPI.COLLIDE_EVENT).Unsubscribe(OnBulletCollided);
            GameObject gameObject = bullet.Get<IAtomicValue<GameObject>>(TransformAPI.GAME_OBJECT).Value;
            GameObject.Destroy(gameObject);
        }

        ~BulletDestroyController()
        {
            _bulletFabric.CreatedBullet -= OnBulletCreated;
        }
    }
}