using Cysharp.Threading.Tasks;
using ShootEmUp.Characters;
using ShootEmUp.Enemies.Agents;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class AttackAfterMovementMechanic
    {
        private readonly CooldownAttackMechanic _attackMechanic;
        private readonly MoveToMechanic _moveToMechanic;

        public AttackAfterMovementMechanic(CooldownAttackMechanic attackMechanic,
            MoveToMechanic moveToMechanic)
        {
            _attackMechanic = attackMechanic;
            _moveToMechanic = moveToMechanic;
        }

        public async UniTask AttackAfterReached(IAttackableTarget target, Vector3 destination)
        {
            await _moveToMechanic.MoveTo(destination);
            _attackMechanic.StartAttacking(target);
        }
    }
}