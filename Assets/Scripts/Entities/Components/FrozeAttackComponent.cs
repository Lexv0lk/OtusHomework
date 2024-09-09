namespace Entities.Components
{
    public struct FrozeAttackComponent
    {
        public int TurnsDuration;
        
        public FrozeAttackComponent(int turnsDuration)
        {
            TurnsDuration = turnsDuration;
        }
    }
}