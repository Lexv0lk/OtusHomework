using ShootEmUp;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private WeaponComponent _weaponComponent;
    [SerializeField] private HitPointsComponent _hitPointsComponent;
    [SerializeField] private TeamComponent _teamComponent;
    [SerializeField] private MoveComponent _moveComponent;

    public BulletSystem.Args GetFireArgs()
    {
        BulletSystem.Args args = _weaponComponent.GetFireArgs();
        args.Team = _teamComponent.Team;
        return args;
    }

    public BulletSystem.Args GetFireArgsAtTarget(Vector2 targetPosition)
    {
        BulletSystem.Args args = _weaponComponent.GetFireArgsAtTarget(targetPosition);
        args.Team = _teamComponent.Team;
        return args;
    }

    public void TakeDamage(int damage)
    {
        _hitPointsComponent.TakeDamage(damage);
    }

    public void MoveByRigidbodyVelocity(Vector2 velocity)
    {
        _moveComponent.MoveByRigidbodyVelocity(velocity);
    }
}