namespace Entities.Components
{
    public struct FrozenTag
    {
        public int TurnsLeft;
        
        public FrozenTag(int turnsLeft)
        {
            TurnsLeft = turnsLeft;
        }
    }
}