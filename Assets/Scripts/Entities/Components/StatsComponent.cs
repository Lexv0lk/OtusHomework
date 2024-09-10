namespace Entities.Components
{
    public struct StatsComponent
    {
        public readonly int MaxHealth;
        
        public int Attack;
        public int CurrentHealth;
        
        public StatsComponent(int attack, int health)
        {
            MaxHealth = health;
            
            Attack = attack;
            CurrentHealth = MaxHealth;
        }
    }
}