using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Pools;
using Game.Scripts.Tech;

namespace Game.Scripts.Controllers
{
    public class EnemyDeathObserver
    {
        private readonly IAtomicEntityPool _enemyPool;

        public EnemyDeathObserver(GamePools gamePools)
        {
            _enemyPool = gamePools.EnemyPool;

            _enemyPool.Given += OnEnemyGiven;
        }

        private void OnEnemyGiven(AtomicEntity enemy)
        {
            enemy.Get<IAtomicEvent<AtomicEntity>>(LifeAPI.DIE_EVENT).Subscribe(EnemyDied);
        }

        private void EnemyDied(AtomicEntity enemy)
        {
            enemy.Get<IAtomicEvent<AtomicEntity>>(LifeAPI.DIE_EVENT).Unsubscribe(EnemyDied);
            _enemyPool.ReleaseEntity(enemy);
        }

        ~EnemyDeathObserver()
        {
            _enemyPool.Given -= OnEnemyGiven;
        }
    }
}