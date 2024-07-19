using ShootEmUp.Characters;

namespace ShootEmUp.Bullets
{
    internal class DamageMechanic
    {
        internal static void DealDamage(Bullet bullet, IDamageable damageable)
        {
            if (bullet.Team == damageable.TeamComponent.Team)
                return;
            
            damageable.HitPointsComponent.TakeDamage(bullet.Damage);
        }
    }
}