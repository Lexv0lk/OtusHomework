using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField] private Team _team;

        public Team Team => _team;
    }
}