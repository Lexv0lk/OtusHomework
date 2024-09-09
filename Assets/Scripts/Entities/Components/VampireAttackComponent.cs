namespace Entities.Components
{
    public struct VampireAttackComponent
    {
        public float Chance;

        public VampireAttackComponent(float chance)
        {
            Chance = chance;
        }
    }
}