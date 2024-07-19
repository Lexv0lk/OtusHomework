using ShootEmUp.Components;

namespace ShootEmUp.Characters
{
    public interface IDamageable
    {
        public HitPointsComponent HitPointsComponent { get; }
        public TeamComponent TeamComponent { get; }
    }
}