using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void DealDamage(Bullet bullet, GameObject other)
        {
            if (other.TryGetComponent(out TeamComponent team) == false)
                return;

            if (bullet.Team == team.Team)
                return;

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.TakeDamage(bullet.Damage);
        }
    }
}