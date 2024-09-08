using UI;
using UnityEngine;

namespace DI
{
    public class TeamViewSetup : MonoBehaviour
    {
        [SerializeField] private HeroListView _blueTeam;
        [SerializeField] private HeroListView _redTeam;

        public HeroListView BlueTeam => _blueTeam;
        public HeroListView RedTeam => _redTeam;
    }
}