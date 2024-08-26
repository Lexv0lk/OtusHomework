using ShootEmUp.Characters;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class EnemyInitializer : MonoBehaviour
    {
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Character _player;

        public void Initialize(Enemy enemy)
        {
            Transform spawnPosition = _enemyPositions.GetRandomSpawnPosition();
            Transform attackPosition = _enemyPositions.GetRandomAttackPosition();

            enemy.transform.position = spawnPosition.position;
            enemy.MoveAgent.SetDestination(attackPosition.position);
            enemy.AttackAgent.SetTarget(_player);
        }
    }
}