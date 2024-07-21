using System;
using Cysharp.Threading.Tasks;
using ShootEmUp.Bullets;
using ShootEmUp.Characters;
using ShootEmUp.Enemies.Agents;
using ShootEmUp.GameUpdate;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class Enemy : Character, IGameFixedUpdateListener
    {
        [SerializeField] private float _moveDestinationAccuracy;
        [SerializeField] private float _fireDelay;
        
        private CooldownAttackMechanic _attackMechanic;
        private MoveToMechanic _moveToMechanic;
        private AttackAfterMovementMechanic _attackAfterMovementMechanic;

        public event Action<BulletSpawner.TeamShootArgs> Attacked;
        
        private void Awake()
        {
            _attackMechanic = new CooldownAttackMechanic(WeaponComponent, TeamComponent, _fireDelay, OnAttacked);
            _moveToMechanic = new MoveToMechanic(MoveComponent, _moveDestinationAccuracy, transform);
            _attackAfterMovementMechanic = new AttackAfterMovementMechanic(_attackMechanic, _moveToMechanic);
        }

        public void Initialize(IAttackableTarget target, Vector3 destination)
        {
            _attackAfterMovementMechanic.AttackAfterReached(target, destination).Forget();
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _moveToMechanic.OnFixedUpdate(fixedDeltaTime);
            _attackMechanic.OnFixedUpdate(fixedDeltaTime);
        }

        private void OnAttacked(BulletSpawner.TeamShootArgs args)
        {
            Attacked?.Invoke(args);
        }
    }
}