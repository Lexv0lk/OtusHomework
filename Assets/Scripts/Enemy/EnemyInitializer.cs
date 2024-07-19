using ShootEmUp.Characters;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class EnemyInitializer
    {
        private readonly EnemyPositions _enemyPositions;
        private readonly Character _player;

        [Inject]
        public EnemyInitializer(EnemyPositions enemyPositions, Character player)
        {
            _enemyPositions = enemyPositions;
            _player = player;
        }

        public void Initialize(Enemy enemy)
        {
            Transform spawnPosition = _enemyPositions.GetRandomSpawnPosition();
            Transform attackPosition = _enemyPositions.GetRandomAttackPosition();

            enemy.transform.position = spawnPosition.position;
            enemy.Initialize(_player, attackPosition.position);
        }
    }
}