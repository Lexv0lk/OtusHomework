using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Fabrics;
using Game.Scripts.Models;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Entities
{
    public class Player : Character
    {
        [Get(ShootAPI.SHOOT_REQUEST)]
        public IAtomicAction ShootRequest => _playerCore.ShootComponent.ShootRequest;
        
        [SerializeField] private PlayerCore _playerCore;

        [SerializeField] private PlayerAnimation _playerAnimation;

        [Inject]
        private void Construct(IBulletFabric fabric, RiffleStoreModel riffleStoreModel)
        {
            _playerCore.ShootComponent.Construct(fabric, riffleStoreModel);
        }

        protected override void OnAwake()
        {
            _playerCore.Compose();
            _playerAnimation.Compose(CharacterCore, CharacterAnimation, transform);
            
            foreach (var mechanic in _playerAnimation.GetMechanics())
                AddLogic(mechanic);
        }

        protected override void OnUpdate()
        {
            _playerCore.Update(Time.deltaTime);
        }

        protected override void OnDispose()
        {
            _playerCore.Dispose();
        }
    }
}