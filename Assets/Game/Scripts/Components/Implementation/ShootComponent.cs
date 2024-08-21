using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Fabrics;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Components
{
    [Serializable]
    public class ShootComponent : ConditionalComponent
    {
        public AtomicEvent ShootRequest;
        public AtomicEvent ShootEvent;
        
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _reloadTime;

        private IBulletFabric _bulletFabric;
        private RiffleStoreModel _riffleStoreModel;
        private float _reloadTimeLeft;
        
        public void Construct(IBulletFabric bulletFabric, RiffleStoreModel riffleStoreModel)
        {
            _bulletFabric = bulletFabric;
            _riffleStoreModel = riffleStoreModel;
        }

        public override void Compose()
        {
            Condition.AddCondition(IsReloaded);
            Condition.AddCondition(IsAmmoEnough);
            
            ShootRequest.Subscribe(Shoot);
        }

        public override void Update(float deltaTime)
        {
            _reloadTimeLeft -= deltaTime;
        }

        private void Shoot()
        {
            if (Condition.IsTrue() == false)
                return;
            
            IAtomicEntity bullet = _bulletFabric.GetBullet();
            
            bullet.Get<IAtomicVariable<Vector3>>(TransformAPI.POSITION).Value = _shootPoint.position;
            bullet.Get<IAtomicVariable<Vector3>>(MoveAPI.MOVE_DIRECTION).Value = _shootPoint.forward;

            _reloadTimeLeft = _reloadTime;
            _riffleStoreModel.AmmunitionAmount.Value--;
            
            ShootEvent.Invoke();
        }

        private bool IsReloaded()
        {
            return _reloadTimeLeft <= 0;
        }

        private bool IsAmmoEnough()
        {
            return _riffleStoreModel.AmmunitionAmount.Value > 0;
        }

        public override void Dispose()
        {
            ShootRequest.Unsubscribe(Shoot);
        }
    }
}