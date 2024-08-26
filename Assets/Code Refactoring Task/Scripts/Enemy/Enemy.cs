using ShootEmUp.Characters;
using ShootEmUp.Enemies.Agents;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private EnemyAttackAgent _attackAgent;
        [SerializeField] private EnemyMoveAgent _moveAgent;

        public Character Character => _character;
        public EnemyAttackAgent AttackAgent => _attackAgent;
        public EnemyMoveAgent MoveAgent => _moveAgent;
    }
}