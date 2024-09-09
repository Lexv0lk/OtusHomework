namespace Entities.Components
{
    public struct RandomTargetComponent
    {
        public float Chance;

        public RandomTargetComponent(float chance)
        {
            Chance = chance;
        }
    }
}