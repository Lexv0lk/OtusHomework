using Entities;
using Utils;

namespace Models
{
    public class TeamsSetup
    {
        public IteratableList<IEntity> RedTeam = new();
        public IteratableList<IEntity> BlueTeam = new();
    }
}