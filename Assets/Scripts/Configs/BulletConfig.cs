using Unity.Entities;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Bullet Config", menuName = "Configs/Bullet Config")]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        public GameObject Prefab => _prefab;
        public int Damage => _damage;
        public float Speed => _speed;
    }
}