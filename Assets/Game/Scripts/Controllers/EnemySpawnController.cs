using Atomic.Elements;
using Atomic.Objects;
using Cysharp.Threading.Tasks;
using Game.Scripts.Configs.Enemies;
using Game.Scripts.Pools;
using Game.Scripts.Tech;
using Game.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class EnemySpawnController : IInitializable
    {
        private readonly EnemySpawnConfig _config;
        private readonly EnemySpawnPositions _enemySpawnPositions;
        private readonly IAtomicEntityPool _pool;

        public EnemySpawnController(EnemySpawnConfig config,
            GamePools gamePools, EnemySpawnPositions enemySpawnPositions)
        {
            _config = config;
            _enemySpawnPositions = enemySpawnPositions;
            _pool = gamePools.EnemyPool;
        }
        
        public void Initialize()
        {
            StartSpawning().Forget();
        }

        private async UniTaskVoid StartSpawning()
        {
            while (true)
            {
                AtomicEntity enemy = _pool.GetEntity();
                enemy.transform.position = _enemySpawnPositions.GetRandomPosition();
                await UniTask.WaitForSeconds(_config.Delay);
            }
        }
    }
}