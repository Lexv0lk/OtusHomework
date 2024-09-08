namespace Entities.Components
{
    public struct StatsComponent
    {
        public int Attack;
        public int Health;
        
        public StatsComponent(int attack, int health)
        {
            Attack = attack;
            Health = health;
        }
    }
}