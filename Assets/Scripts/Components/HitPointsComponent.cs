using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> HitPointsEnded;
        
        [SerializeField] private int _hitPoints;
        
        public bool IsHitPointsExists()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;

            if (_hitPoints <= 0)
                HitPointsEnded?.Invoke(gameObject);
        }
    }
}