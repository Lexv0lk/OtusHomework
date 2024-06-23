using ShootEmUp.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public class BulletLevelBoundsWatcher : MonoBehaviour
    {
        private readonly HashSet<Bullet> _watchTargets = new();
        private readonly List<Bullet> _targetsToWatchInFrame = new List<Bullet>();

        [SerializeField] private LevelBounds _levelBounds;

        public event Action<Bullet> WentOutOfBounds;

        public void AddTargetToWatch(Bullet newTarget)
        {
            _watchTargets.Add(newTarget);
        }

        private void FixedUpdate()
        {
            if (_watchTargets.Count == 0)
                return;

            _targetsToWatchInFrame.Clear();
            _targetsToWatchInFrame.AddRange(_watchTargets);

            for (int i = 0; i < _targetsToWatchInFrame.Count; i++)
            {
                if (_levelBounds.InBounds(_targetsToWatchInFrame[i].transform.position) == false)
                {
                    _watchTargets.Remove(_targetsToWatchInFrame[i]);
                    WentOutOfBounds?.Invoke(_targetsToWatchInFrame[i]);
                }
            }
        }
    }
}