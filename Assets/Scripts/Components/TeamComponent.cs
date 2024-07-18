using ShootEmUp.Common;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class TeamComponent
    {
        [SerializeField] private Team _team;

        public Team Team => _team;
    }
}