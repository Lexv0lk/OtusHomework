using System.Collections.Generic;
using ShootEmUp.GameStates;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class BulletsStateUpdateController : IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        private readonly BulletSpawner _spawner;
        private readonly HashSet<Bullet> _activeBullets = new();

        [Inject]
        public BulletsStateUpdateController(BulletSpawner spawner)
        {
            _spawner = spawner;
            
            _spawner.Spawned += AddBullet;
            _spawner.Released += RemoveBullet;
        }

        ~BulletsStateUpdateController()
        {
            _spawner.Spawned -= AddBullet;
            _spawner.Released -= RemoveBullet;
        }

        private void AddBullet(Bullet bullet)
        {
            _activeBullets.Add(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            _activeBullets.Remove(bullet);
        }
        
        void IGamePauseListener.OnPause()
        {
            foreach (var bullet in _activeBullets)
                bullet.OnPause();
        }

        void IGameResumeListener.OnResume()
        {
            foreach (var bullet in _activeBullets)
                bullet.OnResume();
        }

        void IGameFinishListener.OnFinish()
        {
            foreach (var bullet in _activeBullets)
                bullet.OnFinish();
        }
    }
}