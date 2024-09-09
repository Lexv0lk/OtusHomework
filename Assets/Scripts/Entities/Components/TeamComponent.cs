using Utils;

namespace Entities.Components
{
    public struct TeamComponent
    {
        public Team Team;

        public TeamComponent(Team team)
        {
            Team = team;
        }
    }
}