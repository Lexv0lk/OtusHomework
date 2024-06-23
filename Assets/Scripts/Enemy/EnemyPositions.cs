using UnityEngine;

namespace ShootEmUp.Enemies
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        public Transform GetRandomSpawnPosition()
        {
            return GetRandomTransform(_spawnPositions);
        }

        public Transform GetRandomAttackPosition()
        {
            return GetRandomTransform(_attackPositions);
        }

        private Transform GetRandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}