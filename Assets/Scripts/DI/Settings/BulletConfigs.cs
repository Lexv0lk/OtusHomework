using System;
using ShootEmUp.Bullets;
using UnityEngine;

namespace ShootEmUp.DI.Settings
{
    [Serializable]
    public class BulletConfigs
    {
        [SerializeField] private BulletConfig _playerBulletConfig;
        [SerializeField] private BulletConfig _enemyBulletConfig;

        public BulletConfig PlayerBulletConfig => _playerBulletConfig;
        public BulletConfig EnemyBulletConfig => _enemyBulletConfig;
    }
}