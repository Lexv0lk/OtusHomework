using ShootEmUp.Level;
using System;
using System.Collections.Generic;
using ShootEmUp.GameUpdate;
using Zenject;

namespace ShootEmUp.Bullets
{
    public class BulletLevelBoundsWatcher : IGameFixedUpdateListener
    {
        private readonly HashSet<Bullet> _watchTargets = new();
        private readonly List<Bullet> _targetsToWatchInFrame = new List<Bullet>();
        private readonly LevelBounds _levelBounds;
        private readonly BulletSpawner _spawner;

        [Inject]
        public BulletLevelBoundsWatcher(LevelBounds levelBounds, BulletSpawner spawner)
        {
            _levelBounds = levelBounds;
            _spawner = spawner;
            
            _spawner.Spawned += AddTargetToWatch;
        }

        ~BulletLevelBoundsWatcher()
        {
            _spawner.Spawned -= AddTargetToWatch;
        }
        
        private void AddTargetToWatch(Bullet newTarget)
        {
            _watchTargets.Add(newTarget);
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            if (_watchTargets.Count == 0)
                return;

            _targetsToWatchInFrame.Clear();
            _targetsToWatchInFrame.AddRange(_watchTargets);

            for (int i = 0; i < _targetsToWatchInFrame.Count; i++)
            {
                if (_levelBounds.InBounds(_targetsToWatchInFrame[i].transform.position) == false)
                {
                    var bullet = _targetsToWatchInFrame[i];
                    _watchTargets.Remove(bullet);
                    _spawner.ReleaseBullet(bullet);
                }
            }
        }
    }
}