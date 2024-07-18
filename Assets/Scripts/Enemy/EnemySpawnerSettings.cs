using UnityEngine;

namespace ShootEmUp.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Spawner Settings", menuName = "Enemies/Enemy Spawner Settings")]
    public class EnemySpawnerSettings : ScriptableObject
    {
        [SerializeField] private int _count = 7;
        [SerializeField] private float _spawnDelay = 1;
        [SerializeField] private bool _spawnOnStart;

        public int Count => _count;
        public float SpawnDelay => _spawnDelay;
        public bool SpawnOnStart => _spawnOnStart;
    }
}