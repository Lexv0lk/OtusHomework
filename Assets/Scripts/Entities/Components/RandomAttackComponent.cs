namespace Entities.Components
{
    public struct RandomAttackComponent
    {
        public int Damage;

        public RandomAttackComponent(int damage)
        {
            Damage = damage;
        }
    }
}