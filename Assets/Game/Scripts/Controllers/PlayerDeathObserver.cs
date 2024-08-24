using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class PlayerDeathObserver
    {
        private readonly ShootController _shootController;
        private readonly IAtomicObservable<bool> _isDeadObservable;

        public PlayerDeathObserver(IAtomicEntity player, ShootController shootController)
        {
            _shootController = shootController;
            _isDeadObservable = player.Get<IAtomicObservable<bool>>(LifeAPI.IS_DEAD);
            _isDeadObservable.Subscribe(OnPlayerDied);
        }

        private void OnPlayerDied(bool isDead)
        {
            if (isDead == false)
                return;

            Time.timeScale = 0;
            _shootController.Disable();
        }

        ~PlayerDeathObserver()
        {
            _isDeadObservable.Unsubscribe(OnPlayerDied);
        }
    }
}